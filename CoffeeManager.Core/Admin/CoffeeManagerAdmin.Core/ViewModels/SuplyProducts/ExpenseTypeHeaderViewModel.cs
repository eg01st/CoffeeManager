using System;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;

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
