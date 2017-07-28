using System;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.Managers;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;
using CoffeeManagerAdmin.Core.ViewModels.Orders;
using System.Diagnostics;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ShiftManager _shiftManager = new ShiftManager();


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
        public ICommand EditProductCalculation => _editProductCalculation;
        public ICommand ShowStatiscticCommand => _showStatiscticCommand;
        public ICommand ShowExpensesCommand {get;set;} 

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
        public MainViewModel()
        {
            _showShiftsCommand = new MvxCommand(DoShowShifts);
            _showSupliedProductsCommand = new MvxCommand(DoShowSupliedProducts);
            _updateEntireMoneyCommand = new MvxCommand(DoGetEntireMoney);
            _showOrdersCommand = new MvxCommand(DoShowOrders);
            _editProductsCommand = new MvxCommand(DoEditProducts);
            _editUsersCommand = new MvxCommand(DoEditUsers);
            _editProductCalculation = new MvxCommand(DoEditCalculation);
            _showStatiscticCommand = new MvxCommand(() => ShowViewModel<StatisticViewModel>());
            ShowExpensesCommand = new MvxCommand(() => ShowViewModel<ManageExpensesViewModel>());
        }

        private void DoEditCalculation()
        {
            ShowViewModel<ProductsCalculationViewModel>();
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
            var currentBalance = await _shiftManager.GetEntireMoney();
            Debug.WriteLine("currentBalance "+ currentBalance);
            CurrentBalance = currentBalance.ToString("F1");
            var shiftBalance = await _shiftManager.GetCurrentShiftMoney();
            Debug.WriteLine("shiftBalance "+ shiftBalance);
            CurrentShiftBalance = shiftBalance.ToString("F1");
        }
    }
}
