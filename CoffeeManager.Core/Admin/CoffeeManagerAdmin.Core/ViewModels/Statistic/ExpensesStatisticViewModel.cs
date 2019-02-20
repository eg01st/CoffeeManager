using System;
using System.Collections.Generic;
using CoffeeManager.Models;
using System.Linq;
using CoffeManager.Common;
using System.Threading.Tasks;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Statistic
{
    public class ExpensesStatisticViewModel : BaseSearchViewModel<ExpenseItemViewModel>
    {
        readonly IStatisticManager manager;
        readonly DateTime from;
        readonly DateTime to;
        private readonly int coffeeRoomId;

        public ExpensesStatisticViewModel(IStatisticManager manager, DateTime from, DateTime to, int coffeeRoomId)
        {
            this.to = to;
            this.coffeeRoomId = coffeeRoomId;
            this.from = from;
            this.manager = manager;
        }

        public async override Task<List<ExpenseItemViewModel>> LoadData()
        {
            IEnumerable<Expense> expenses = await manager.GetExpenses(coffeeRoomId, from, to);
            var resutl = expenses.Select(s=> new ExpenseItemViewModel(s)).ToList();
            return resutl;
        }
    }
}
