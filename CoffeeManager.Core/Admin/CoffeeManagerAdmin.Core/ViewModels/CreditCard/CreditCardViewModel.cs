using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MobileCore.Extensions;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.CreditCard
{
    public class CreditCardViewModel : ViewModelBase
    {
        private decimal amountToCashOut;
        private decimal currentAmount;
        private List<CashoutHistoryItemViewModel> cashoutItems;

        readonly IPaymentManager manager;

        public decimal AmountToCashOut
        {
            get { return amountToCashOut; }
            set
            {
                amountToCashOut = value;
                RaisePropertyChanged(nameof(AmountToCashOut));
            }
        }

        public decimal CurrentAmount
        {
            get { return currentAmount; }
            set
            {
                currentAmount = value;
                RaisePropertyChanged(nameof(CurrentAmount));
            }
        }

        public List<CashoutHistoryItemViewModel> CashoutItems
        {
            get { return cashoutItems; }
            set
            {
                cashoutItems = value;
                RaisePropertyChanged(nameof(CashoutItems));
            }
        }


        public ICommand CashoutCommand { get; }

        public ICommand SetAmountCommand { get; }

        public MvxAsyncCommand<CashoutHistoryItemViewModel> ItemSelectedCommand { get; }
        
        public CreditCardViewModel(IPaymentManager manager)
        {
            this.manager = manager;
            CashoutCommand = new MvxCommand(DoCashOut);
            SetAmountCommand = new MvxCommand(DoSetAmount);
            ItemSelectedCommand = new MvxAsyncCommand<CashoutHistoryItemViewModel>(OnItemSelectedAsync);

        }
        
        private async Task OnItemSelectedAsync(CashoutHistoryItemViewModel item)
        {
            item.ThrowIfNull(nameof(item));
            
            item.SelectCommand.Execute();

            await Task.Yield();
        }

        public override async Task Initialize()
        {
            var amount = await ExecuteSafe(manager.GetCreditCardEntireMoney);
            CurrentAmount = amount;
            var items = await ExecuteSafe(manager.GetCashoutHistory);
            CashoutItems = items.Select(s => new CashoutHistoryItemViewModel(s)).OrderByDescending(o => o.Id).ToList();
        }

        private void DoSetAmount()
        {
            Confirm($"Записать {CurrentAmount} грн как текущий баланс карты?", SetAmount);
        }

        private async Task SetAmount()
        {
            await manager.SetCreditCardEntireMoney(CurrentAmount);
            await Initialize();
            Publish(new UpdateCashAmountMessage(this));
        }


        private void DoCashOut()
        {
            if(AmountToCashOut < 0)
            {
                return;
            }
            if(AmountToCashOut > CurrentAmount)
            {
                Alert("Сумма обналички больше чем текушая сумма!");
                return;
            }

            Confirm($"Обналичить {AmountToCashOut} грн с карты?", CashOut);
        }

        private async Task CashOut()
        {
            await manager.CashOutCreditCard(AmountToCashOut);
            await Initialize();
            AmountToCashOut = 0;
            Publish(new UpdateCashAmountMessage(this));
        }

    }
}
