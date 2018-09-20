using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeManager.Common;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels.Settings
{
    public class SettingsViewModel : PageViewModel
    {
        private bool isInitialSetup;
        private bool isLoggedIn;

        public List<CoffeeRoomItemViewModel> CoffeeRooms {get;set;}

        public ICommand CoffeeRoomSelectedCommand { get; set; }

        readonly IAdminManager adminManager;
        readonly ILocalStorage localStorage;

        public SettingsViewModel(IAdminManager adminManager, ILocalStorage localStorage)
        {
            this.localStorage = localStorage;
            this.adminManager = adminManager;
            CoffeeRoomSelectedCommand = new MvxCommand<CoffeeRoomItemViewModel>(DoSelectItem);
        }

        public override async Task Initialize()
        {
            await ExecuteSafe(async () =>
            {
                var coffeeRomms = await adminManager.GetCoffeeRooms();
                CoffeeRooms = coffeeRomms.Select(s => new CoffeeRoomItemViewModel(s.Id, s.Name)).ToList();
            });
            var currentCoffeeRoom = localStorage.GetCoffeeRoomId();
            if (currentCoffeeRoom != -1)
            {
                var coffeeRoomVm = CoffeeRooms.First(c => c.Id == currentCoffeeRoom);
                coffeeRoomVm.IsSelected = true;
            }
            else
            {
                isInitialSetup = true;
            }
            RaiseAllPropertiesChanged();
        }


        private void DoSelectItem(CoffeeRoomItemViewModel obj)
        {
            localStorage.SetCoffeeRoomId(obj.Id);
            foreach (var item in CoffeeRooms)
            {
                item.IsSelected = false;
            }
            obj.IsSelected = true;
        }

        protected override async Task DoClose()
        {
            var coffeeRoomId = localStorage.GetCoffeeRoomId();
            if (coffeeRoomId == -1)
            {
                Alert("Выберите кофейню для работы с программой");
                return;
            }
            CloseCommand.Execute(null);
            await NavigationService.Navigate<SplashViewModel>();
        }
    }
}
