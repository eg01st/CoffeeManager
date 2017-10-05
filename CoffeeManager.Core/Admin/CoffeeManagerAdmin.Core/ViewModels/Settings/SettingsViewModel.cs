using System;
using CoffeManager.Common;
using System.Windows.Input;
using System.Threading.Tasks;
using CoffeManager.Common.Managers;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core
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

        readonly IAccountManager manager;
        readonly IAdminManager adminManager;

        public SettingsViewModel(IAccountManager manager, IAdminManager adminManager)
        {
            this.adminManager = adminManager;
            this.manager = manager;

            AddUserCommand = new MvxCommand(DoAddUser);
            AddCoffeeRoomCommand = new MvxCommand(DoAddCoffeeRoom);

            refreshUsersToken = Subscribe<RefreshAdminUsersMessage>(async (obj) => await Init());
            refreshCoffeeroomsToken = Subscribe<RefreshCoffeeRoomsMessage>(async (obj) => await Init());
        }

        private async void DoAddCoffeeRoom()
        {
            await ExecuteSafe(async () =>
            {
                if(string.IsNullOrWhiteSpace(NewCoffeeroomName))
                {
                    Alert("Укажите адресс кофейни!");
                    return;
                }
                await adminManager.AddCoffeeRoom(NewCoffeeroomName);
                Publish(new RefreshCoffeeRoomsMessage(this));
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
