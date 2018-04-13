using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ViewModels.Shifts;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class ExpenseItemViewModel : ListItemViewModelBase
    {
        private Expense _item;
        private bool canRemove;
        public ExpenseItemViewModel(Expense item, bool canRemove = false)
        {
            this.canRemove = canRemove;
            _item = item;
            DeleteExpenseCommand = new MvxCommand(DoDeleExpense);
        }

        private void DoDeleExpense()
        {
            if(canRemove)
            {
                Confirm($"Удалить расход {Name}?", DeleteExpense);
            }
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
