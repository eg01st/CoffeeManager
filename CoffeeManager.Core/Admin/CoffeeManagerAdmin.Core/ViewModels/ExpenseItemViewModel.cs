using System;
using CoffeeManager.Models;
using CoffeManager.Common;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;
using MvvmCross.Platform;

namespace CoffeeManagerAdmin.Core
{
    public class ExpenseItemViewModel : ListItemViewModelBase
    {
        private Expense _item;
        public ExpenseItemViewModel(Expense item)
        {
            _item = item;
            DeleteExpenseCommand = new MvxCommand(DoDeleExpense);
        }

        private void DoDeleExpense()
        {
            Confirm($"Удалить расход {Name}?", DeleteExpense);
        }

        private async Task DeleteExpense()
        {
            var manager = Mvx.Resolve<IPaymentManager>();
            await ExecuteSafe(manager.DeleteExpenseItem(Id));
            Publish(new UpdateShiftMessage(this));
            Publish(new UpdateCashAmountMessage(this));
        }

        public ICommand DeleteExpenseCommand { get;}

        public int Id => _item.Id;

        public override string Name => _item.Name;

        public string Amount => _item.Amount.ToString("F");

        public int ItemCount => _item.ItemCount;

        protected override void DoGoToDetails()
        {
            if(Id > 0)
            {
                ShowViewModel<ShiftExpenseDetailsViewModel>(new {id = Id});
            }
        }
    }
}
