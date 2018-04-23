using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeManager.Common;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.Abstract
{
    public class AdminPageViewModel : PageViewModel
    {
        private MvxSubscriptionToken coffeeRoomUpdatedToken;
        private MvxSubscriptionToken refreshCoffeeroomsToken;

        private List<Entity> coffeeRooms;

        public AdminPageViewModel()
        {
            SelectCoffeeRoomCommand = new MvxCommand(DoSelectCoffeeRoom);
        }
        
        public ICommand SelectCoffeeRoomCommand { get; }
        
        public bool IsMultipleCoffeeRooms => CoffeeRooms.Count > 1;

        public virtual bool ShouldReloadOnCoffeeRoomChange => false;

        public virtual Entity CurrentCoffeeRoom
        {
            get { return CoffeeRooms.FirstOrDefault(c => c.Id == Config.CoffeeRoomNo); }
            set
            {
                if (value == null || Config.CoffeeRoomNo == value.Id)
                {
                    return;
                }
                Config.CoffeeRoomNo = value.Id;
                RaisePropertyChanged(nameof(CurrentCoffeeRoom));
                RaisePropertyChanged(nameof(CurrentCoffeeRoomName));
                MvxMessenger.Publish(new CoffeeRoomChangedMessage(this));
            }
        }

        public List<Entity> CoffeeRooms
        {
            get => coffeeRooms;
            set
            {
                coffeeRooms = value;
                RaisePropertyChanged(nameof(CoffeeRooms));
            }
        }

        public string CurrentCoffeeRoomName => CurrentCoffeeRoom.Name;

        private async Task InitCoffeeRooms()
        {
            await ExecuteSafe(async () =>
            {
                var adminManager = Mvx.Resolve<IAdminManager>();
                var items = await adminManager.GetCoffeeRooms();
                CoffeeRooms = items.ToList();
                RaisePropertyChanged(nameof(CurrentCoffeeRoom));
                RaisePropertyChanged(nameof(IsMultipleCoffeeRooms));
            });
        }
        
        private void DoSelectCoffeeRoom()
        {
            if (CoffeeRooms.Count <= 1)
            {
                return;
            }
            var optionList = new List<ActionSheetOption>();
            foreach (var cr in CoffeeRooms)
            {
                optionList.Add(new ActionSheetOption(cr.Name, () => { CurrentCoffeeRoom = CoffeeRooms.First(c => c.Id == cr.Id); }));
            }

            UserDialogs.ActionSheet(new ActionSheetConfig
            {
                Options = optionList,
                Title = "Выбор заведения",
            });
        }

        protected override async Task DoPreLoadDataImplAsync()
        {
            await InitCoffeeRooms();
        }

        protected override void DoSubscribe()
        {
            base.DoSubscribe();

            coffeeRoomUpdatedToken = MvxMessenger.Subscribe<CoffeeRoomChangedMessage>(async (s) =>
            {
                if (ShouldReloadOnCoffeeRoomChange && IsMultipleCoffeeRooms)
                {
                    await Initialize();
                }
            });
            refreshCoffeeroomsToken = MvxMessenger.Subscribe<RefreshCoffeeRoomsMessage>(async (obj) => await InitCoffeeRooms());
        }

        protected override void DoUnsubscribe()
        {
            base.DoUnsubscribe();
            MvxMessenger.Unsubscribe<CoffeeRoomChangedMessage>(coffeeRoomUpdatedToken);
            MvxMessenger.Unsubscribe<RefreshCoffeeRoomsMessage>(refreshCoffeeroomsToken);
        }
    }
}