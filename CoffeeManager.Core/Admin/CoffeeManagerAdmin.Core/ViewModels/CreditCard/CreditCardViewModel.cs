using System;
using CoffeManager.Common;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
namespace CoffeeManagerAdmin.Core
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

        public CreditCardViewModel(IPaymentManager manager)
        {
            this.manager = manager;
            CashoutCommand = new MvxCommand(DoCashOut);
            SetAmountCommand = new MvxCommand(DoSetAmount);
        }

        public async Task Init()
        {
            var amount = await ExecuteSafe(manager.GetCreditCardEntireMoney);
            CurrentAmount = amount;
            var items = await ExecuteSafe(manager.GetCashoutHistory);
            CashoutItems = items.Select(s => new CashoutHistoryItemViewModel(s)).ToList();
        }

        private void DoSetAmount()
        {
            Confirm($"Записать {CurrentAmount} грн как текущий баланс карты?", SetAmount);
        }

        private async Task SetAmount()
        {
            await manager.SetCreditCardEntireMoney(CurrentAmount);
            await Init();
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
            await Init();
            AmountToCashOut = 0;
            Publish(new UpdateCashAmountMessage(this));
        }

    }
}
