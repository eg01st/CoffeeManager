using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.ServiceProviders;
using CoffeeManager.Models;

namespace CoffeeManager.Core.Managers
{
    public class PaymentManager : BaseManager
    {
        private PaymentServiceProvider provider = new PaymentServiceProvider();

        public async Task<decimal> GetCurrentShiftMoney()
        {
            return await provider.GetCurrentShiftMoney();
        }

        public async Task<decimal> GetEntireMoney()
        {
            return await provider.GetEntireMoney();
        }

        public async Task AddExpense(int expenseId, decimal amout, int itemCount)
        {
            await provider.AddExpense(ShiftNo, expenseId, amout, itemCount);
        }

        public async Task<Entity[]> GetExpenseItems()
        {
            return await provider.GetExpenseItems();
        }

        public async Task AddNewExpenseType(string typeName)
        {
            await provider.AddNewExpenseType(typeName);
        }

        public async Task<Expense[]> GetShiftExpenses()
        {
            return await provider.GetShiftExpenses(ShiftNo);
        }

        public async Task DeleteExpenseItem(int id)
        {
            await provider.DeleteExpenseItem(id);
        }
    }
}
