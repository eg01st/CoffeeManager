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
        readonly IStatisticManager manager;
        readonly DateTime from;
        readonly DateTime to;

        public ExpensesStatisticViewModel(IStatisticManager manager, DateTime from, DateTime to)
        {
            this.to = to;
            this.from = from;
            this.manager = manager;
        }

        public async override Task<List<ExpenseItemViewModel>> LoadData()
        {
            IEnumerable<Expense> expenses = await manager.GetExpenses(from, to);
            var resutl = expenses.Select(s=> new ExpenseItemViewModel(s)).ToList();
            return resutl;
        }
    }
}
