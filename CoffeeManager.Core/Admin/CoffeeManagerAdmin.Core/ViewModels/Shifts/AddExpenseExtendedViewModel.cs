using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Shifts
{
    public class AddExpenseExtendedViewModel : BaseSearchViewModel<AddExtendedExpenseItemViewModel>, IMvxViewModel<ExpenseType>
    {
        ExpenseType expenseType;
        readonly IPaymentManager manager;

        public ICommand SaveExpenseCommand { get; }

        public AddExpenseExtendedViewModel(IPaymentManager manager)
        {
            this.manager = manager;

            SaveExpenseCommand = new MvxAsyncCommand(DoSaveExpense);
        }

        private async Task DoSaveExpense()
        {
            var itemsToSend = Items.Where(i => i.Quantity > 0 && i.Amount > 0);

            if(!itemsToSend.Any())
            {
                DoClose();
                return;
            }

            var expense = new ExpenseType();
            var products = new List<SupliedProduct>();
            expense.Id = expenseType.Id;

            foreach (var item in itemsToSend)
            {
                products.Add(new SupliedProduct(){Id = item.Id, Quatity = item.Quantity, Price = item.Amount});
            }
            expense.SuplyProducts = products.ToArray();

            await ExecuteSafe(manager.AddExpense(expense));
            MvxMessenger.Publish(new UpdateCashAmountMessage(this));
            MvxMessenger.Publish(new UpdateShiftMessage(this));
            DoClose();
        }

        public override async Task<List<AddExtendedExpenseItemViewModel>> LoadData()
        {
            return expenseType.SuplyProducts.Select(s => new AddExtendedExpenseItemViewModel(s)).ToList();
        }

        public void Prepare(ExpenseType parameter)
        {
            expenseType = parameter;
        }
    }
}
