using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ViewModels;
using CoffeManager.Common;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core
{
    public class MoneyViewModel : ViewModelBase
    {
        private readonly MvxSubscriptionToken refreshCoffeeroomsToken;
        private readonly MvxSubscriptionToken refreshAmountToken;

        private string _currentBalance;
        private string _currentShiftBalance;
        private string _currentCreditCardBalance;
        private Entity _currentCoffeeRoom;
        private List<Entity> coffeeRooms;
        readonly IShiftManager shiftManager;
        readonly IAdminManager adminManager;
        readonly IPaymentManager paymentManager;

        public MoneyViewModel(IShiftManager shiftManager, IAdminManager adminManager, IPaymentManager paymentManager)
        {
            this.paymentManager = paymentManager;
            this.adminManager = adminManager;
            this.shiftManager = shiftManager;
            ShowShiftsCommand = new MvxCommand(() => ShowViewModel<ShiftsViewModel>());

            UpdateEntireMoneyCommand = new MvxAsyncCommand(DoGetEntireMoney);
            ShowProductsCommand = new MvxCommand(() => ShowViewModel<ProductsViewModel>());
            ShowExpensesCommand = new MvxCommand(() => ShowViewModel<ManageExpensesViewModel>());
            ShowSettingsCommand = new MvxCommand(() => ShowViewModel<SettingsViewModel>());
            ShowCreditCardCommand = new MvxCommand(() => ShowViewModel<CreditCardViewModel>());

            refreshCoffeeroomsToken = Subscribe<RefreshCoffeeRoomsMessage>(async (obj) => await GetCoffeeRooms());
            refreshAmountToken = Subscribe<UpdateCashAmountMessage>(async (obj) => await GetEntireMoney());
        }

        public ICommand UpdateEntireMoneyCommand { get; }
        public ICommand ShowShiftsCommand { get; }
        public ICommand ShowProductsCommand { get; }
        public ICommand ShowExpensesCommand { get; }
        public ICommand ShowCreditCardCommand { get; }
        public ICommand ShowSettingsCommand { get; }


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

        public string CurrentCreditCardBalance
        {
            get { return _currentCreditCardBalance; }
            set
            {
                _currentCreditCardBalance = value;
                RaisePropertyChanged(nameof(CurrentCreditCardBalance));
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


        public async Task Init()
        {
            await GetEntireMoney();
            await GetCoffeeRooms();
        }

        private async Task DoGetEntireMoney()
        {
            await GetEntireMoney();
        }

        private async Task GetEntireMoney()
        {
            await ExecuteSafe(async () =>
            {
                var currentBalance = await paymentManager.GetEntireMoney();
                CurrentBalance = currentBalance.ToString("F1");
                var shiftBalance = await paymentManager.GetCurrentShiftMoney();
                CurrentShiftBalance = shiftBalance.ToString("F1");
                var creditCardBalance = await paymentManager.GetCreditCardEntireMoney();
                CurrentCreditCardBalance = creditCardBalance.ToString("F1");
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
