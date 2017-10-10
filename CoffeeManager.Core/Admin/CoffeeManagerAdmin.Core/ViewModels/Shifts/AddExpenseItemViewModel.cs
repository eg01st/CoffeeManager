using System;
using CoffeManager.Common;
using CoffeeManager.Models;
using MvvmCross.Platform;
using CoffeeManagerAdmin.Core.Util;

namespace CoffeeManagerAdmin.Core
{
    public class AddExpenseItemViewModel : ListItemViewModelBase
    {
        private ExpenseType item;

        public AddExpenseItemViewModel(ExpenseType item)
        {
            Name = item.Name;
            this.item = item;
        }

        protected async override void DoGoToDetails()
        {
            if(item.SuplyProducts.Length == 0)
            {
                var quantity = await PromtAsync("Введите количество:");
                if(!quantity.HasValue)
                {
                    return;
                }
                var amount = await PromtDecimalAsync("Введите общую сумму:");
                if(!amount.HasValue)
                {
                    return;
                }
                var manager = Mvx.Resolve<IPaymentManager>();

                await ExecuteSafe(manager.AddExpense(item.Id, amount.Value, quantity.Value));
                Publish(new UpdateCashAmountMessage(this));
                Publish(new UpdateShiftMessage(this)); 
            }
            else
            {
                var id = ParameterTransmitter.PutParameter(item);
                ShowViewModel<AddExpenseExtendedViewModel>(new {id = id});
            }
        }
    }
}
