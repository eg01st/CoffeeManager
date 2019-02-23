using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeManager.Common.Managers;
using MobileCore.Collections;
using MobileCore.Extensions;
using MobileCore.ViewModels;
using MobileCore.ViewModels.Items;

namespace CoffeeManagerAdmin.Core.ViewModels.Statistic
{
    public class SalesStatisticViewModel : FeedViewModel<FeedItemElementViewModel>
    {
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

        protected override async Task OnItemSelectedAsync(FeedItemElementViewModel item)
        {
            if (item is SectionHeaderItemViewModel sectionVm)
            {
                this.ToggleCollapse(sectionVm);
                return;
            }
            
            await base.OnItemSelectedAsync(item);
        }

        protected override Task DataLoadedAsync()
        {
            foreach (var header in ItemsCollection.OfType<SectionHeaderItemViewModel>().ToList())
            {
                this.ToggleCollapse(header);
            }
            return base.DataLoadedAsync();
        }

        protected override async Task<PageContainer<FeedItemElementViewModel>> GetPageAsync(int skip)
        {
           var items = new List<FeedItemElementViewModel>();
            var saleItems = (await manager.GetSales(coffeeRoomId, from, to)).ToList();

            var entireAmount = saleItems.Sum(i => i.Amount);
            var entireAmountHeaderVm = new SectionHeaderItemViewModel("Общая сумма", entireAmount.Value.ToString("F"));
            items.Add(entireAmountHeaderVm);

            var categories = await categoryManager.GetCategoriesPlain();

            var groupedByProductType = saleItems.GroupBy(g => g.Producttype);
            foreach (var item in groupedByProductType)
            {
                var name = categories.First(t => t.Id == item.Key).Name;
                var sum = item.Sum(s => s.Amount);
                var amountByProductType = new SectionHeaderItemViewModel(name, sum.Value.ToString("F"), true);
                items.Add(amountByProductType);
                items.AddRange(item.Select(s => new StatisticSaleItemViewModel(s)));
            }
            return items.ToPageContainer();
        }
    }
}
