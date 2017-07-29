using System;
using CoffeeManager.Models;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;
using CoffeeManagerAdmin.Core.Managers;

namespace CoffeeManagerAdmin.Core
{
  public class ManageExpenseItemViewModel : ListItemViewModelBase
    {
        PaymentManager pm = new PaymentManager();

        public int Id {get;set;}
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
