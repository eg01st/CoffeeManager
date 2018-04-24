using System;
using System.Collections.Generic;
using CoffeeManager.Models;
using System.Linq;
using CoffeManager.Common;
using System.Threading.Tasks;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Statistic
{
    public class SalesStatisticViewModel : ViewModelBase
    {
        private IEnumerable<SaleInfo> saleItems;

        public List<BaseStatisticSaleItemViewModel> Items {get;set;}


        readonly IStatisticManager manager;
        readonly DateTime from;
        readonly DateTime to;

        public SalesStatisticViewModel(IStatisticManager manager, DateTime from, DateTime to)
        {
            this.to = to;
            this.from = from;
            this.manager = manager;
        }

        public override async Task Initialize()
        {
            saleItems = await manager.GetSales(from, to);
            
            var entireAmount = saleItems.Sum(i => i.Amount);
            var entireAmountHeaderVm = new StatisticSaleHeaderViewModel("Общая сумма", entireAmount.Value);
            Items = new List<BaseStatisticSaleItemViewModel>();
            Items.Add(entireAmountHeaderVm);
            
            var groupedByProductType = saleItems.GroupBy(g => g.Producttype);
            foreach (var item in groupedByProductType)
            {
                var name = TypesLists.ProductTypesList.First(t => t.Id == item.Key).Name;
                var sum = item.Sum(s => s.Amount);
                var amountByProductType = new StatisticSaleHeaderViewModel(name, sum.Value);
                Items.Add(amountByProductType);
                Items.AddRange(item.Select(s=> new StatisticSaleItemViewModel(s)));
            }
            RaisePropertyChanged(nameof(Items));
        }


    }
}
