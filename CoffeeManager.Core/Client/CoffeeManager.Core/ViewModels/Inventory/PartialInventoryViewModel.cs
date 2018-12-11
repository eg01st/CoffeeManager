using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeManager.Common.Common;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels.Inventory
{
    public class PartialInventoryViewModel : FeedViewModel<PartialInventoryItemViewModel>, IMvxViewModel<List<SupliedProduct>, List<SupliedProduct>>
    {
        public void Prepare(List<SupliedProduct> parameter)
        {
            ItemsCollection.ReplaceWith(parameter.Select(p => new PartialInventoryItemViewModel(p)));
        }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        protected override async Task DoClose()
        {
            if (ItemsCollection.All(i => i.IsProceeded))
            {
                base.DoOnClose();
            }
            else
            {
                Alert(Strings.NeedToProceedAllInventoryItemsMessage);
            }
        }

        protected override async Task PerformClose()
        {
            await NavigationService.Close(this, ItemsCollection.Select(s => s.Entity).ToList());
        }
    }
}