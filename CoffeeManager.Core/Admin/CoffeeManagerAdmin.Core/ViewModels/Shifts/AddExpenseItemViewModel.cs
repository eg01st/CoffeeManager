using CoffeeManager.Models;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Platform;

namespace CoffeeManagerAdmin.Core.ViewModels.Shifts
{
    public class AddExpenseItemViewModel : ListItemViewModelBase
    {
        private ExpenseType item;

        public AddExpenseItemViewModel(ExpenseType item)
        {
            Name = item.Name;
            this.item = item;
        }

        protected override async void DoGoToDetails()
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
                await NavigationService.Navigate<AddExpenseExtendedViewModel, ExpenseType>(item);
            }
        }
    }
}
