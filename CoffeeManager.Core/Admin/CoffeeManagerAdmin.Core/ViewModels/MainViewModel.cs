using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;
using CoffeeManagerAdmin.Core.ViewModels.Orders;
using CoffeManager.Common;
using CoffeeManager.Core.ViewModels;
using System.Collections.Generic;
using CoffeeManager.Models;
using System.Linq;
using CoffeeManager.Common;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IShiftManager shiftManager;

        private ICommand _showShiftsCommand;
        private ICommand _showSupliedProductsCommand;
        private ICommand _updateEntireMoneyCommand;
        private ICommand _showOrdersCommand;
        private ICommand _editProductsCommand;
        private ICommand _editUsersCommand;
        private ICommand _showStatiscticCommand;

        public void ShowErrorMessage(string v)
        {
            UserDialogs.Alert(v);
        }


        private string _currentBalance;
        private string _currentShiftBalance;
        private Entity _currentCoffeeRoom;
        private List<Entity> coffeeRooms;

        public ICommand ShowShiftsCommand => _showShiftsCommand;
        public ICommand ShowSupliedProductsCommand => _showSupliedProductsCommand;
        public ICommand UpdateEntireMoneyCommand => _updateEntireMoneyCommand;
        public ICommand ShowOrdersCommand => _showOrdersCommand;
        public ICommand EditProductsCommand => _editProductsCommand;
        public ICommand EditUsersCommand => _editUsersCommand;
        public ICommand ShowStatiscticCommand => _showStatiscticCommand;
        public ICommand ShowExpensesCommand { get; set; }
        public ICommand ShowInventoryCommand { get; set; }
        public ICommand ShowUtilizedSuplyProductsCommand { get; set; }

        public string CurrentBalance
        {
            get { return _currentBalance; }
            set
            {
                _currentBalance = value;
                RaisePropertyChanged(nameof(CurrentBalance));
            }
        }

        public string CurrentShiftBalance
        {
            get { return _currentShiftBalance; }
            set
            {
                _currentShiftBalance = value;
                RaisePropertyChanged(nameof(CurrentShiftBalance));
            }
        }

        public Entity CurrentCoffeeRoom
        {
            get { return _currentCoffeeRoom; }
            set
            {
                _currentCoffeeRoom = value;
                RaisePropertyChanged(nameof(CurrentCoffeeRoom));
                RaisePropertyChanged(nameof(CurrentCoffeeRoomName));
                Config.CoffeeRoomNo = _currentCoffeeRoom.Id;
                GetEntireMoney();
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

        readonly IAdminManager adminManager;

        public MainViewModel(IShiftManager shiftManager, IAdminManager adminManager)
        {
            this.adminManager = adminManager;
            this.shiftManager = shiftManager;
            _showShiftsCommand = new MvxCommand(DoShowShifts);
            _showSupliedProductsCommand = new MvxCommand(DoShowSupliedProducts);
            _updateEntireMoneyCommand = new MvxCommand(DoGetEntireMoney);
            _showOrdersCommand = new MvxCommand(DoShowOrders);
            _editProductsCommand = new MvxCommand(DoEditProducts);
            _editUsersCommand = new MvxCommand(DoEditUsers);
            _showStatiscticCommand = new MvxCommand(() => ShowViewModel<StatisticViewModel>());
            ShowExpensesCommand = new MvxCommand(() => ShowViewModel<ManageExpensesViewModel>());
            ShowInventoryCommand = new MvxCommand(() => ShowViewModel<InventoryViewModel>());
            ShowUtilizedSuplyProductsCommand = new MvxCommand(() => ShowViewModel<UtilizeViewModel>());
        }


        private void DoEditUsers()
        {
            ShowViewModel<UsersViewModel>();
        }

        private void DoEditProducts()
        {
            ShowViewModel<ProductsViewModel>();
        }

        private void DoShowOrders()
        {
            ShowViewModel<OrdersViewModel>();
        }

        private void DoShowSupliedProducts()
        {
            ShowViewModel<SuplyProductsViewModel>();
        }

        private void DoShowShifts()
        {
            ShowViewModel<ShiftsViewModel>();
        }

        public async void Init()
        {
            await GetEntireMoney();
            await GetCoffeeRooms();

        }

        private async void DoGetEntireMoney()
        {
            await GetEntireMoney();
        }

        private async Task GetEntireMoney()
        {
            await ExecuteSafe(async () =>
            {
                var currentBalance = await shiftManager.GetEntireMoney();
                CurrentBalance = currentBalance.ToString("F1");
                var shiftBalance = await shiftManager.GetCurrentShiftMoney();
                CurrentShiftBalance = shiftBalance.ToString("F1");
            });
        }

        private async Task GetCoffeeRooms()
        {
            await ExecuteSafe(async () =>
            {
                var items = await adminManager.GetCoffeeRooms();
                CoffeeRooms = items.ToList();
                CurrentCoffeeRoom = CoffeeRooms.First();
            });
        }
    }
}
