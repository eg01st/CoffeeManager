using System;
using CoffeeManager.Models;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
namespace CoffeeManagerAdmin.Core
{
  using CoffeeManagerAdmin.Core.Managers;
  public class ManageExpenseItemViewModel : ListItemViewModelBase
    {
        PaymentManager pm = new PaymentManager();

        public int Id {get;set;}
        public string Name {get;set;}
        public bool IsActive {get;set;}

        public ICommand ToggleIsActiveCommand {get;set;}
        public ManageExpenseItemViewModel(ExpenseType e)
        {
            ToggleIsActiveCommand = new MvxCommand(DoToggleIsActive);
            Id = e.Id;
            Name = e.Name;
            IsActive = e.IsActive;
            RaiseAllPropertiesChanged();
        }

        private async void DoToggleIsActive()
        {
            await pm.ToggleIsActiveExpense(Id);
        }
   }
}
