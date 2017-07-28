using System;
using System.Collections.Generic;
using CoffeeManager.Models;
using System.Linq;
using CoffeeManagerAdmin.Core.Util;

namespace CoffeeManagerAdmin.Core.ViewModels.Statistic
{
    public class ExpensesStatisticViewModel : ViewModelBase
    {
        public IEnumerable<ExpenseItemViewModel> Items { get { return items;} set { items = value; RaisePropertyChanged(nameof(Items));}}

        private IEnumerable<ExpenseItemViewModel> items;

        public void Init(Guid id)
        {
            IEnumerable<Expense> expenses;
            ParameterTransmitter.TryGetParameter(id, out expenses);
            Items = expenses.Select(s=> new ExpenseItemViewModel(s));
        }
    }
}
