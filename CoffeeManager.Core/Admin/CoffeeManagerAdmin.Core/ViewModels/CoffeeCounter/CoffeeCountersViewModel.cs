using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeManager.Common.Managers;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.CoffeeCounter
{
    public class CoffeeCountersViewModel : FeedViewModel<CoffeeCounterItemViewModel>
    {
        private MvxSubscriptionToken listChangedToken;
        
        private readonly ICoffeeCounterManager coffeeCounterManager;

        public ICommand AddCounterCommand { get; }
        
        public CoffeeCountersViewModel(ICoffeeCounterManager coffeeCounterManager)
        {
            this.coffeeCounterManager = coffeeCounterManager;
            
            AddCounterCommand = new MvxAsyncCommand(DoAddCounter);
        }

        protected override void DoSubscribe()
        {
            base.DoSubscribe();
            listChangedToken = MvxMessenger.Subscribe<CoffeeRoomChangedMessage>(async (s) => await Initialize());
        }

        protected override void DoUnsubscribe()
        {
            base.DoUnsubscribe();
            MvxMessenger.Unsubscribe<CoffeeRoomChangedMessage>(listChangedToken);
        }

        private async Task DoAddCounter()
        {
            await NavigationService.Navigate<AddCoffeeCounterViewModel>();
        }

        protected override async Task DoLoadDataImplAsync()
        {
            var dtos = await coffeeCounterManager.GetCounters();
            ItemsCollection.AddRange(dtos.Select(s => new CoffeeCounterItemViewModel(s)));
        }
    }
}