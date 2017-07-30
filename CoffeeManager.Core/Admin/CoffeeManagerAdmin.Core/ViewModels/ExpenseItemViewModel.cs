using System;
using CoffeeManager.Models;
using CoffeManager.Common;
namespace CoffeeManagerAdmin.Core
{
    public class ExpenseItemViewModel : ListItemViewModelBase
    {
        private Expense _item;
        public ExpenseItemViewModel(Expense item)
        {
            _item = item;
        }

        public override string Name => _item.Name;

        public string Amount => _item.Amount.ToString("F");

        public int ItemCount => _item.ItemCount;
    }
}
