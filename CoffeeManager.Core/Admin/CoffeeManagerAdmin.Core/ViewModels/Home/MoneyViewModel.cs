using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeManager.Common;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.Home
{
    public class MoneyViewModel : ViewModelBase
    {
        private readonly MvxSubscriptionToken refreshCoffeeroomsToken;
        private readonly MvxSubscriptionToken refreshAmountToken;

        private bool isNextPageLoading = false;

        private MvxObservableCollection<ShiftItemViewModel> items = new MvxObservableCollection<ShiftItemViewModel>();
        private string currentBalance;
        private string currentShiftBalance;
        private string currentCreditCardBalance;
        private Entity currentCoffeeRoom;
        private List<Entity> coffeeRooms;
        readonly IShiftManager shiftManager;
        readonly IAdminManager adminManager;
        readonly IPaymentManager paymentManager;

        public MoneyViewModel(IShiftManager shiftManager, IAdminManager adminManager, IPaymentManager paymentManager)
        {
            this.paymentManager = paymentManager;
            this.adminManager = adminManager;
            this.shiftManager = shiftManager;

            UpdateEntireMoneyCommand = new MvxAsyncCommand(DoGetEntireMoney);
            ShowSettingsCommand = new MvxCommand(() => ShowViewModel<SettingsViewModel>());
            ShowUsersCommand = new MvxCommand(() => ShowViewModel<UsersViewModel>());
            ShowCreditCardCommand = new MvxCommand(() => ShowViewModel<CreditCardViewModel>());
            LoadNextPageCommand = new MvxAsyncCommand(DoLoadNextPage);

            refreshCoffeeroomsToken = Subscribe<RefreshCoffeeRoomsMessage>(async (obj) => await GetCoffeeRooms());
            refreshAmountToken = Subscribe<UpdateCashAmountMessage>(async (obj) => await GetEntireMoney());
        }

        private async Task DoLoadNextPage()
        {
            if (isNextPageLoading == true)
            {
                return;
            }

            isNextPageLoading = true;

            
            var itemsToSkip = Items.Count;
            var shifts = await ExecuteSafe(async () => await shiftManager.GetShifts(itemsToSkip));
            var vms = shifts.Select(s => new ShiftItemViewModel(s));

            Items.AddRange(vms);

            isNextPageLoading = false;

        }

        public MvxObservableCollection<ShiftItemViewModel> Items
        {
            get => items;
            private set
            {
                items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        public ICommand UpdateEntireMoneyCommand { get; }
        public ICommand ShowCreditCardCommand { get; }
        public ICommand ShowSettingsCommand { get; }
        public ICommand ShowUsersCommand { get; }
        public ICommand LoadNextPageCommand { get; }

        public string CurrentBalance
        {
            get => currentBalance;
            private set
            {
                currentBalance = value;
                RaisePropertyChanged(nameof(CurrentBalance));
            }
        }

        public string CurrentShiftBalance
        {
            get => currentShiftBalance;
            private set
            {
                currentShiftBalance = value;
                RaisePropertyChanged(nameof(CurrentShiftBalance));
            }
        }

        public string CurrentCreditCardBalance
        {
            get => currentCreditCardBalance;
            private set
            {
                currentCreditCardBalance = value;
                RaisePropertyChanged(nameof(CurrentCreditCardBalance));
            }
        }

        public Entity CurrentCoffeeRoom
        {
            get => currentCoffeeRoom;
            private set
            {
                currentCoffeeRoom = value;
                RaisePropertyChanged(nameof(CurrentCoffeeRoom));
                RaisePropertyChanged(nameof(CurrentCoffeeRoomName));
                Config.CoffeeRoomNo = currentCoffeeRoom.Id;
                GetEntireMoney();
                Publish(new CoffeeRoomChangedMessage(this));
            }
        }

        public List<Entity> CoffeeRooms
        {
            get => coffeeRooms;
            private set
            {
                coffeeRooms = value;
                RaisePropertyChanged(nameof(CoffeeRooms));
            }
        }

        public string CurrentCoffeeRoomName => CurrentCoffeeRoom.Name;


        public async Task Init()
        {
            await GetEntireMoney();
            await GetCoffeeRooms();
            await ExecuteSafe(GetShifts);
        }

        private async Task GetShifts()
        {
            Items.Clear();
            var shifts = await shiftManager.GetShifts(0);
            if (shifts != null)
            {
                Items.AddRange(shifts.Select(s => new ShiftItemViewModel(s)));
            }
            else
            {
                UserDialogs.Alert("Empty list from server");
            }
        }

        private async Task DoGetEntireMoney()
        {
            await GetEntireMoney();
        }

        private async Task GetEntireMoney()
        {
            await ExecuteSafe(async () =>
            {
                var currBalance = await paymentManager.GetEntireMoney();
                CurrentBalance = currBalance.ToString("F1");
                var shiftBalance = await paymentManager.GetCurrentShiftMoney();
                CurrentShiftBalance = shiftBalance.ToString("F1");
                var creditCardBalance = await paymentManager.GetCreditCardEntireMoney();
                CurrentCreditCardBalance = creditCardBalance.ToString("F1");
                await GetShifts();
            });
        }

        private async Task GetCoffeeRooms()
        {
            await ExecuteSafe(async () =>
            {
                var rooms = await adminManager.GetCoffeeRooms();
                CoffeeRooms = rooms.ToList();
                CurrentCoffeeRoom = CoffeeRooms.First();
            });
        }
    }
}
