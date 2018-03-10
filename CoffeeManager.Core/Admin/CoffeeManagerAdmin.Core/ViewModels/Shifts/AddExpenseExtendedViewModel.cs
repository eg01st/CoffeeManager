using System;
using CoffeManager.Common;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Util;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
namespace CoffeeManagerAdmin.Core
{
    public class AddExpenseExtendedViewModel : BaseSearchViewModel<AddExtendedExpenseItemViewModel>
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
            Publish(new UpdateCashAmountMessage(this));
            Publish(new UpdateShiftMessage(this));
            DoClose();
        }

        public void Init(Guid id)
        {
            ParameterTransmitter.TryGetParameter(id, out expenseType);

        }

        public async override Task<List<AddExtendedExpenseItemViewModel>> LoadData()
        {
            return expenseType.SuplyProducts.Select(s => new AddExtendedExpenseItemViewModel(s)).ToList();
        }
    }
}
