using System;
using CoffeManager.Common;
using System.Collections.Generic;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using CoffeeManager.Core.ViewModels;

namespace CoffeeManager.Core
{
    public class SettingsViewModel : ViewModelBase
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

        public async Task Init(bool isInitialSetup)
        {
            this.isInitialSetup = isInitialSetup;

            if(!isInitialSetup)
            {
                isLoggedIn = await PromtLogin();
                if (isLoggedIn == false)
                {
                    CloseCommand.Execute(null);
                    return;
                }
            }

            await ExecuteSafe(async () =>
            {
                var coffeeRomms = await adminManager.GetCoffeeRooms();
                CoffeeRooms = coffeeRomms.Select(s => new CoffeeRoomItemViewModel(s.Id, s.Name)).ToList();
            });
            var currentCoffeeRoom = localStorage.GetCoffeeRoomId();
            if(currentCoffeeRoom != -1)
            {
                var coffeeRoomVm = CoffeeRooms.First(c => c.Id == currentCoffeeRoom);
                coffeeRoomVm.IsSelected = true;
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

        protected override void DoClose()
        {
            if(!isInitialSetup && !isLoggedIn)
            {
                base.DoClose();
                return;
            }
            var coffeeRoomId = localStorage.GetCoffeeRoomId();
            if (coffeeRoomId == -1)
            {
                Alert("Выберите кофейню для работы с программой");
                return;
            }
            ShowViewModel<LoginViewModel>();
            base.DoClose();
        }
    }
}
