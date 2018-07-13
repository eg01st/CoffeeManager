using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.ViewModels.Abstract;
using CoffeeManagerAdmin.Core.ViewModels.CreditCard;
using CoffeeManagerAdmin.Core.ViewModels.Settings;
using CoffeeManagerAdmin.Core.ViewModels.Shifts;
using CoffeManager.Common.Managers;
using MobileCore.Collections;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.Home
{
    public class MoneyViewModel : AdminCoffeeRoomFeedViewModel<ShiftItemViewModel>
    {
        private readonly MvxSubscriptionToken refreshAmountToken;
        
        private string currentBalance;
        private string currentShiftBalance;
        private string currentCreditCardBalance;

        readonly IShiftManager shiftManager;
        readonly IPaymentManager paymentManager;

        public MoneyViewModel(IShiftManager shiftManager, IPaymentManager paymentManager)
        {
            this.paymentManager = paymentManager;
            this.shiftManager = shiftManager;

            UpdateEntireMoneyCommand = new MvxAsyncCommand(RefreshDataAsync);
            ShowSettingsCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<SettingsViewModel>());
            ShowUsersCommand = new MvxAsyncCommand(async() => await NavigationService.Navigate<UsersViewModel>());
            ShowCreditCardCommand = new MvxAsyncCommand(async() => await NavigationService.Navigate<CreditCardViewModel>());
            
            refreshAmountToken = MvxMessenger.Subscribe<UpdateCashAmountMessage>(async (obj) => await GetEntireMoney());
        }

        public ICommand UpdateEntireMoneyCommand { get; }
        public ICommand ShowCreditCardCommand { get; }
        public ICommand ShowSettingsCommand { get; }
        public ICommand ShowUsersCommand { get; }

        public override bool ShouldReloadOnCoffeeRoomChange => true;
        
        public string CurrentBalance
        {
            get => currentBalance;
            set => SetProperty(ref currentBalance, value);
        }

        public string CurrentShiftBalance
        {
            get => currentShiftBalance;
            set => SetProperty(ref currentShiftBalance, value);
        }

        public string CurrentCreditCardBalance
        {
            get => currentCreditCardBalance;
            set => SetProperty(ref currentCreditCardBalance, value);
        }

        protected override async Task DoPreLoadDataImplAsync()
        {
            await base.DoPreLoadDataImplAsync();
            await GetEntireMoney();
        }
        
        protected override async Task<PageContainer<ShiftItemViewModel>> GetPageAsync(int skip)
        {
            var shifts = await ExecuteSafe(async () => await shiftManager.GetShifts(skip));
            var vms = shifts.Select(s => new ShiftItemViewModel(s));
            return vms.ToPageContainer();
        }

        private async Task GetEntireMoney()
        {
            await ExecuteSafe(async () =>
            {
                var currBalanceTask = paymentManager.GetEntireMoney();
                var shiftBalanceTask = paymentManager.GetCurrentShiftMoney();
                var creditCardBalanceTask = paymentManager.GetCreditCardEntireMoney();

                await Task.WhenAll(currBalanceTask, shiftBalanceTask, creditCardBalanceTask);

                var currBalance = currBalanceTask.Result;
                CurrentBalance = currBalance.ToString("F1");
                var shiftBalance = shiftBalanceTask.Result;
                CurrentShiftBalance = shiftBalance.ToString("F1");
                var creditCardBalance = creditCardBalanceTask.Result;
                CurrentCreditCardBalance = creditCardBalance.ToString("F1");
            });
        }
    }
}
