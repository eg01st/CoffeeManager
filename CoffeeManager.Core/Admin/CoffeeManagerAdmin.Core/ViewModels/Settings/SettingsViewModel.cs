using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MobileCore.Extensions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using CoffeeManagerAdmin.Core.ViewModels.CoffeeCounter;

namespace CoffeeManagerAdmin.Core.ViewModels.Settings
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly MvxSubscriptionToken refreshUsersToken;
        private readonly MvxSubscriptionToken refreshCoffeeroomsToken;

        private string newCoffeeroomName;

        private bool isAdmin;
        private List<ClientItemViewModel> clients;
        private List<CoffeeRoomItemViewModel> coffeeRooms;

        public string NewCoffeeroomName
        {
            get { return newCoffeeroomName; }
            set
            {
                newCoffeeroomName = value;
                RaisePropertyChanged(nameof(NewCoffeeroomName));
            }
        }

        public bool IsAdmin
        {
            get { return isAdmin; }
            set
            {
                isAdmin = value;
                RaisePropertyChanged(nameof(IsAdmin));
            }
        }

        public List<ClientItemViewModel> Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                RaisePropertyChanged(nameof(Clients));
            }
        }

        public List<CoffeeRoomItemViewModel> CoffeeRooms
        {
            get { return coffeeRooms; }
            set
            {
                coffeeRooms = value;
                RaisePropertyChanged(nameof(CoffeeRooms));
            }
        }

        public ICommand AddUserCommand { get; set; }

        public ICommand AddCoffeeRoomCommand { get; set; }

        public ICommand ShowCountersCommand { get; set; }

        readonly IAccountManager manager;
        readonly IAdminManager adminManager;

        public MvxAsyncCommand<CoffeeRoomItemViewModel> ItemSelectedCommand { get; }
        
        public SettingsViewModel(IAccountManager manager, IAdminManager adminManager)
        {
            this.adminManager = adminManager;
            this.manager = manager;

            ShowCountersCommand =
                new MvxAsyncCommand(async () => await NavigationService.Navigate<CoffeeCountersViewModel>());

            AddUserCommand = new MvxCommand(DoAddUser);
            AddCoffeeRoomCommand = new MvxCommand(DoAddCoffeeRoom);
            ItemSelectedCommand = new MvxAsyncCommand<CoffeeRoomItemViewModel>(OnItemSelectedAsync);
            
            refreshUsersToken = Subscribe<RefreshAdminUsersMessage>(async (obj) => await Init());
            refreshCoffeeroomsToken = Subscribe<RefreshCoffeeRoomsMessage>(async (obj) => await Init());
        }
        
        private async Task OnItemSelectedAsync(CoffeeRoomItemViewModel item)
        {
            item.ThrowIfNull(nameof(item));
            
            item.SelectCommand.Execute();

            await Task.Yield();
        }

        private async void DoAddCoffeeRoom()
        {
            await ExecuteSafe(async () =>
            {
                if(string.IsNullOrWhiteSpace(NewCoffeeroomName))
                {
                    Alert("Укажите адрес заведения!");
                    return;
                }
                await adminManager.AddCoffeeRoom(NewCoffeeroomName);
                Publish(new RefreshCoffeeRoomsMessage(this));
                NewCoffeeroomName = string.Empty;
            });
        }

        private void DoAddUser()
        {
            ShowViewModel<CreateClientViewModel>();
        }

        public async Task Init()
        {
            await ExecuteSafe(async () =>
            {
                var coffeeRooms = await adminManager.GetCoffeeRooms();
                CoffeeRooms = coffeeRooms.Select(s => new CoffeeRoomItemViewModel(s.Id, s.Name)).ToList();

                var userInfo = await manager.GetUserInfo();
                IsAdmin = userInfo.IsAdmin;

                if (isAdmin)
                {
                    var users = await manager.GetUsers();
                    Clients = users.Select(s => new ClientItemViewModel(s)).ToList();
                }
            });

        }
        protected override void DoUnsubscribe()
        {
            Unsubscribe<RefreshAdminUsersMessage>(refreshUsersToken);
            Unsubscribe<RefreshCoffeeRoomsMessage>(refreshCoffeeroomsToken);
        }
    }
}
