using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeManager.Common.Common;
using MobileCore.Collections;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;
using MobileCore;

namespace CoffeeManager.Core.ViewModels.Inventory
{
    public class PartialInventoryViewModel : FeedViewModel<PartialInventoryItemViewModel>, IMvxViewModel<List<SupliedProduct>, List<SupliedProduct>>
    {
        private List<SupliedProduct> items;

        public IMvxAsyncCommand DoneCommand { get; }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        public void Prepare(List<SupliedProduct> parameter)
        {
            items = parameter;
            items.ForEach(i => i.Quatity = 0);
        }

        protected override Task<PageContainer<PartialInventoryItemViewModel>> GetPageAsync(int skip)
        {
            return Task.FromResult(items.Select(p => new PartialInventoryItemViewModel(p)).ToPageContainer());
        }

        public PartialInventoryViewModel()
        {
            DoneCommand = new MvxAsyncCommand(DoDone);

        }

        private async Task DoDone()
        {
            if (ItemsCollection.All(i => i.IsProceeded))
            {
                CloseCommand.Execute(null);
            }
            else
            {
                Alert(Strings.NeedToProceedAllInventoryItemsMessage);
            }
        }

        protected override async Task PerformClose()
        {
            await NavigationService.Close<List<SupliedProduct>>(this, ItemsCollection.Select(s => s.Entity).ToList());
        }
    }
}