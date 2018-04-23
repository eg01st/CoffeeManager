using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ViewModels.CreditCard;
using CoffeeManagerAdmin.Core.ViewModels.Settings;
using CoffeeManagerAdmin.Core.ViewModels.Shifts;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MobileCore.Extensions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using CoffeeManagerAdmin.Core.ViewModels.Abstract;
using MobileCore.Collections;

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

            UpdateEntireMoneyCommand = new MvxAsyncCommand(GetEntireMoney);
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
                var currBalance = await paymentManager.GetEntireMoney();
                CurrentBalance = currBalance.ToString("F1");
                var shiftBalance = await paymentManager.GetCurrentShiftMoney();
                CurrentShiftBalance = shiftBalance.ToString("F1");
                var creditCardBalance = await paymentManager.GetCreditCardEntireMoney();
                CurrentCreditCardBalance = creditCardBalance.ToString("F1");
            });
        }
    }
}
