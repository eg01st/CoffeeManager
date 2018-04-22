using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Messages;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.CoffeeCounter
{
    public class CoffeeCountersViewModel : FeedViewModel<CoffeeCounterItemViewModel>
    {
        private Entity currentCoffeeRoom;
        private List<Entity> coffeeRooms;

        private MvxSubscriptionToken listChangedToken;
        private MvxSubscriptionToken listUpdateToken;
        
        private readonly ICoffeeCounterManager coffeeCounterManager;

        public ICommand AddCounterCommand { get; }

        public Entity CurrentCoffeeRoom
        {
            get { return currentCoffeeRoom; }
            set
            {
                if (currentCoffeeRoom?.Id == value?.Id)
                {
                    return;
                }
                bool isInitialSelect = currentCoffeeRoom == null;
                currentCoffeeRoom = value;
                Config.CoffeeRoomNo = currentCoffeeRoom.Id;
                if(!isInitialSelect)
                {
                    MvxMessenger.Publish(new CoffeeRoomChangedMessage(this));
                }
                RaisePropertyChanged(nameof(CurrentCoffeeRoom));
                RaisePropertyChanged(nameof(CurrentCoffeeRoomName));

            }
        }

        public List<Entity> CoffeeRooms
        {
            get { return coffeeRooms; }
            set
            {
                coffeeRooms = value;
                RaisePropertyChanged(nameof(CoffeeRooms));
            }
        }

        public string CurrentCoffeeRoomName
        {
            get { return CurrentCoffeeRoom.Name; }

        }

        private readonly IAdminManager adminManager;

        public CoffeeCountersViewModel(ICoffeeCounterManager coffeeCounterManager, IAdminManager adminManager)
        {
            this.adminManager = adminManager;
            this.coffeeCounterManager = coffeeCounterManager;
            
            AddCounterCommand = new MvxAsyncCommand(DoAddCounter);
        }

        private async Task InitCoffeeRooms()
        {
            if(CoffeeRooms != null)
            {
                return;
            }
            await ExecuteSafe(async () =>
            {
                var items = await adminManager.GetCoffeeRooms();
                CoffeeRooms = items.ToList();
                CurrentCoffeeRoom = CoffeeRooms.First(c => c.Id == Config.CoffeeRoomNo);
            });
        }

        protected override void DoSubscribe()
        {
            base.DoSubscribe();
            listUpdateToken = MvxMessenger.Subscribe<CoffeeCountersUpdateMessage>(async (s) => await Initialize());
            listChangedToken = MvxMessenger.Subscribe<CoffeeRoomChangedMessage>(async (s) => await Initialize());
        }

        protected override void DoUnsubscribe()
        {
            base.DoUnsubscribe();
            MvxMessenger.Unsubscribe<CoffeeRoomChangedMessage>(listChangedToken);
            MvxMessenger.Unsubscribe<CoffeeCountersUpdateMessage>(listUpdateToken);
        }

        private async Task DoAddCounter()
        {
            await NavigationService.Navigate<AddCoffeeCounterViewModel>();
        }

        protected override async Task DoLoadDataImplAsync()
        {
            await InitCoffeeRooms();

            ItemsCollection.Clear();
            var dtos = await coffeeCounterManager.GetCounters();
            ItemsCollection.AddRange(dtos.Select(s => new CoffeeCounterItemViewModel(s)));
        }
    }
}