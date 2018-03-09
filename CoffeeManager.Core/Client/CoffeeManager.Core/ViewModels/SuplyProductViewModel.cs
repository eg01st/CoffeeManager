using System;
using CoffeManager.Common;
using CoffeeManager.Models;
using System.Drawing;
using CoffeManager.Common.ViewModels;

namespace CoffeeManager.Core
{
    public class SuplyProductViewModel : ViewModelBase
    {
        private decimal itemCount;
        private decimal amount;
        private string name;
        private int id;
        private string expenseMultiplierName;

        public SuplyProductViewModel(SupliedProduct product)
        {
            Id = product.Id;
            Name = product.Name;
            ExpenseMultiplierName = product.ExpenseNumerationName;
            Product = product;
        }

        public SupliedProduct Product { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public string ExpenseMultiplierName
        {
            get { return expenseMultiplierName; }
            set
            {
                expenseMultiplierName = value;
                RaisePropertyChanged(nameof(ExpenseMultiplierName));
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

        public decimal ItemCount
        {
            get { return itemCount; }
            set
            {
                itemCount = value;
                RaisePropertyChanged(nameof(ItemCount));
            }
        }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }
    }
}
