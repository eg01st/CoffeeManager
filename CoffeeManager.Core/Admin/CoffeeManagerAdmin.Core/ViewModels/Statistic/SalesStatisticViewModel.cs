using System;
using System.Collections.Generic;
using CoffeeManager.Models;
using System.Linq;
using CoffeManager.Common;
using System.Threading.Tasks;
using CoffeManager.Common.ViewModels;
using CoffeManager.Common.Managers;

namespace CoffeeManagerAdmin.Core.ViewModels.Statistic
{
    public class SalesStatisticViewModel : ViewModelBase
    {
        private IEnumerable<SaleInfo> saleItems;

        public List<BaseStatisticSaleItemViewModel> Items {get;set;}


        readonly IStatisticManager manager;
        readonly DateTime from;
        readonly DateTime to;
        private readonly int coffeeRoomId;
        readonly ICategoryManager categoryManager;

        public SalesStatisticViewModel(IStatisticManager manager, ICategoryManager categoryManager, DateTime from, DateTime to, int coffeeRoomId)
        {
            this.categoryManager = categoryManager;
            this.to = to;
            this.coffeeRoomId = coffeeRoomId;
            this.from = from;
            this.manager = manager;
        }

        public override async Task Initialize()
        {
            await ExecuteSafe(async () =>
            {
                saleItems = await manager.GetSales(coffeeRoomId, from, to);

                var entireAmount = saleItems.Sum(i => i.Amount);
                var entireAmountHeaderVm = new StatisticSaleHeaderViewModel("Общая сумма", entireAmount.Value);
                Items = new List<BaseStatisticSaleItemViewModel>();
                Items.Add(entireAmountHeaderVm);

                var categories = await categoryManager.GetCategoriesPlain();

                var groupedByProductType = saleItems.GroupBy(g => g.Producttype);
                foreach (var item in groupedByProductType)
                {
                    var name = categories.First(t => t.Id == item.Key).Name;
                    var sum = item.Sum(s => s.Amount);
                    var amountByProductType = new StatisticSaleHeaderViewModel(name, sum.Value);
                    Items.Add(amountByProductType);
                    Items.AddRange(item.Select(s => new StatisticSaleItemViewModel(s)));
                }
                RaisePropertyChanged(nameof(Items));
            });
        }


    }
}
