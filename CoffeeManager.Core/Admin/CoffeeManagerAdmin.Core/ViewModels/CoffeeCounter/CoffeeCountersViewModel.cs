using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Messages;
using CoffeeManagerAdmin.Core.ViewModels.Abstract;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using MobileCore.Collections;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.CoffeeCounter
{
    public class CoffeeCountersViewModel : AdminCoffeeRoomFeedViewModel<CoffeeCounterItemViewModel>
    {
        private MvxSubscriptionToken listUpdateToken;
        
        private readonly ICoffeeCounterManager coffeeCounterManager;

        public override bool ShouldReloadOnCoffeeRoomChange => true;

        public ICommand AddCounterCommand { get; }

        public CoffeeCountersViewModel(ICoffeeCounterManager coffeeCounterManager)
        {
            this.coffeeCounterManager = coffeeCounterManager;
            
            AddCounterCommand = new MvxAsyncCommand(DoAddCounter);
        }

        protected override void DoSubscribe()
        {
            base.DoSubscribe();
            listUpdateToken = MvxMessenger.Subscribe<CoffeeCountersUpdateMessage>(async (s) => await Initialize());
        }

        protected override void DoUnsubscribe()
        {
            base.DoUnsubscribe();
            MvxMessenger.Unsubscribe<CoffeeCountersUpdateMessage>(listUpdateToken);
        }

        private async Task DoAddCounter()
        {
            await NavigationService.Navigate<AddCoffeeCounterViewModel>();
        }

        protected override async Task<PageContainer<CoffeeCounterItemViewModel>> GetPageAsync(int skip)
        {
            var dtos = await coffeeCounterManager.GetCounters();
            return dtos.Select(s => new CoffeeCounterItemViewModel(s)).ToPageContainer();
        }
    }
}