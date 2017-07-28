using System;
using CoffeeManager.Models;
namespace CoffeeManagerAdmin.Core
{
    public class ExpenseItemViewModel : ViewModelBase
    {
        private Expense _item;
        public ExpenseItemViewModel(Expense item)
        {
            _item = item;
        }

        public string Name => _item.Name;

        public string Amount => _item.Amount.ToString("F");

        public int ItemCount => _item.ItemCount;
    }
}
