using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;
using CoffeeManagerAdmin.Core.ViewModels.Orders;
using CoffeManager.Common;
using CoffeeManager.Core.ViewModels;

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
        private ICommand _editProductCalculation;
        private ICommand _showStatiscticCommand;

        public void ShowErrorMessage(string v)
        {
            UserDialogs.Alert(v);
        }


        private string _currentBalance;
        private string _currentShiftBalance;

        public ICommand ShowShiftsCommand => _showShiftsCommand;
        public ICommand ShowSupliedProductsCommand => _showSupliedProductsCommand;
        public ICommand UpdateEntireMoneyCommand => _updateEntireMoneyCommand;
        public ICommand ShowOrdersCommand => _showOrdersCommand;
        public ICommand EditProductsCommand => _editProductsCommand;
        public ICommand EditUsersCommand => _editUsersCommand;
        public ICommand ShowStatiscticCommand => _showStatiscticCommand;
        public ICommand ShowExpensesCommand { get; set; }


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

       

        public MainViewModel(IShiftManager shiftManager)
        {
            this.shiftManager = shiftManager;
            _showShiftsCommand = new MvxCommand(DoShowShifts);
            _showSupliedProductsCommand = new MvxCommand(DoShowSupliedProducts);
            _updateEntireMoneyCommand = new MvxCommand(DoGetEntireMoney);
            _showOrdersCommand = new MvxCommand(DoShowOrders);
            _editProductsCommand = new MvxCommand(DoEditProducts);
            _editUsersCommand = new MvxCommand(DoEditUsers);
            _showStatiscticCommand = new MvxCommand(() => ShowViewModel<StatisticViewModel>());
            ShowExpensesCommand = new MvxCommand(() => ShowViewModel<ManageExpensesViewModel>());
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
    }
}
