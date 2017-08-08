using CoffeeManager.Models;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core
{
    public class ManageExpenseItemViewModel : ListItemViewModelBase
    {

        public int Id {get;set;}
        public bool IsActive {get;set;}

        public ICommand ToggleIsActiveCommand {get;set;}
        private readonly IPaymentManager manager;

        public ManageExpenseItemViewModel(IPaymentManager manager, ExpenseType e)
        {
            this.manager = manager;
            ToggleIsActiveCommand = new MvxCommand(DoToggleIsActive);
            Id = e.Id;
            Name = e.Name;
            IsActive = e.IsActive;
            RaiseAllPropertiesChanged();
        }

        private async void DoToggleIsActive()
        {
            await ExecuteSafe(async () =>
            {
                await manager.ToggleIsActiveExpense(Id);
            });
        }
   }
}
