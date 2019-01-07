﻿using CoffeeManager.Models;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;
using System;
using CoffeeManagerAdmin.Core.ViewModels.ManageExpenses;
using CoffeManager.Common.Managers;
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
            Confirm($"Удалить тип расхода {Name}?", () => DeleteExpenseType());
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

        protected override async void DoGoToDetails()
        {
           await NavigationService.Navigate<ExpenseTypeDetailsViewModel, ExpenseType>(expenseType);
        }
   }
}
