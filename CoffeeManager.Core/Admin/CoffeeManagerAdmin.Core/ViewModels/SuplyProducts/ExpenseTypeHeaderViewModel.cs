using System;
using CoffeManager.Common;
namespace CoffeeManagerAdmin.Core
{
    public class ExpenseTypeHeaderViewModel : ListItemViewModelBase
    {
        public ExpenseTypeHeaderViewModel(string name)
        {
            Name = name;
        }
    }
}
