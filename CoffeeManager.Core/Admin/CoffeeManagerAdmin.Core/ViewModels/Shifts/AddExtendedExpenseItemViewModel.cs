using System;
using CoffeManager.Common;
using CoffeeManager.Models;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class AddExtendedExpenseItemViewModel : ListItemViewModelBase
    {
        private decimal quantity;
        private decimal amount;

        public decimal Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                RaisePropertyChanged(nameof(Quantity));
            }
        }

        public decimal Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                RaisePropertyChanged(nameof(Amount));
            }
        }

        public string ExpenseNumerationName { get; set; }
        public int Id { get; set; }

        public AddExtendedExpenseItemViewModel(SupliedProduct product)
        {
            Name = product.Name;
            ExpenseNumerationName = product.ExpenseNumerationName;
            Id = product.Id;
        }
    }
}
