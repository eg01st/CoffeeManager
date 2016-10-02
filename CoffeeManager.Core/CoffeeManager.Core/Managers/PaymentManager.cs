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

        public async Task AddExpense(int expenseId, float amout)
        {
            await provider.AddExpense(ShiftNo, expenseId, amout);
        }

        public async Task<Entity[]> GetExpenseItems()
        {
            return await provider.GetExpenseItems();

            return new Entity[]
            {
                new Entity { Id = 1, Name = "Кофе"},
                new Entity { Id = 1, Name = "Молоко"},
                new Entity { Id = 1, Name = "Панини"},
            };
        }

        public async Task AddNewExpenseType(string typeName)
        {
            await provider.AddNewExpenseType(typeName);
        }
    }
}
