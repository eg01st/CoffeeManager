using System;
using System.Collections.Generic;
using CoffeeManager.Models;
using System.Linq;
using CoffeeManagerAdmin.Core.Util;
using CoffeManager.Common;
using System.Threading.Tasks;

namespace CoffeeManagerAdmin.Core.ViewModels.Statistic
{
    public class ExpensesStatisticViewModel : BaseSearchViewModel<ExpenseItemViewModel>
    {
        private Guid id;
        public void Init(Guid id)
        {
            this.id = id;
        }

        public override Task<List<ExpenseItemViewModel>> LoadData()
        {
            IEnumerable<Expense> expenses;
            ParameterTransmitter.TryGetParameter(id, out expenses);
            var resutl = expenses.Select(s=> new ExpenseItemViewModel(s)).ToList();
            return Task.FromResult(resutl);
        }
    }
}
