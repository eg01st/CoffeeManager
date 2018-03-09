using CoffeeManager.Models;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;
using System;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class ManageExpenseItemViewModel : ListItemViewModelBase
    {

        public int Id {get;set;}
        public bool IsActive {get;set;}

        public ICommand ToggleIsActiveCommand {get;set;}
        public ICommand DeleteExpenseTypeCommand { get; set; }
        private readonly IPaymentManager manager;
        private readonly ExpenseType expenseType;

        public ManageExpenseItemViewModel(IPaymentManager manager, ExpenseType e)
        {
            this.manager = manager;
            expenseType = e;
            ToggleIsActiveCommand = new MvxCommand(DoToggleIsActive);
            DeleteExpenseTypeCommand = new MvxCommand(DoDeleteExpenseType);
            Id = e.Id;
            Name = e.Name;
            IsActive = e.IsActive;
            RaiseAllPropertiesChanged();
        }

        private void DoDeleteExpenseType()
        {
            Confirm($"Удалить тип траты {Name}?", () => DeleteExpenseType());
        }

        private async void DeleteExpenseType()
        {
            await manager.RemoveExpenseType(Id);
            Publish(new ExpenseListChangedMessage(this));
        }

        private async void DoToggleIsActive()
        {
            await ExecuteSafe(async () =>
            {
                await manager.ToggleIsActiveExpense(Id);
            });
        }

        protected override void DoGoToDetails()
        {
            var id = Util.ParameterTransmitter.PutParameter(expenseType);
            ShowViewModel<ExpenseTypeDetailsViewModel>(new {id});
        }
   }
}
