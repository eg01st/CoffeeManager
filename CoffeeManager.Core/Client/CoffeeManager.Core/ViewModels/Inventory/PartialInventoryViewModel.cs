using System;
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

        public PartialInventoryViewModel()
        {
            DoneCommand = new MvxAsyncCommand(DoDone);

        }

        public override async Task Initialize()
        {
            await base.Initialize();
            RaiseAllPropertiesChanged();
        }

        private async Task DoDone()
        {
            if (ItemsCollection.All(i => i.IsProceeded))
            {
                await NavigationService.Close(this, ItemsCollection.Select(s => s.Entity).ToList());
            }
            else
            {
                Alert(Strings.NeedToProceedAllInventoryItemsMessage);
            }
        }

        protected override async Task DoClose()
        {
            await base.DoClose();
        }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        public IMvxAsyncCommand DoneCommand { get; }
    }
}