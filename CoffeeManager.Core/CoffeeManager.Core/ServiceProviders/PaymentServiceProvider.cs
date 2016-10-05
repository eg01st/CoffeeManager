using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ServiceProviders
{
    public class PaymentServiceProvider : BaseServiceProvider
    {
        private const string Payment = "Payment";

        public async Task<decimal> GetCurrentShiftMoney()
        {
            return await Get<decimal>($"{Payment}/getcurrentshiftmoney", null);
        }

        public async Task<decimal> GetEntireMoney()
        {
            return await Get<decimal>($"{Payment}/getentiremoney", null);
        }

        public async Task AddExpense(int shiftId, int expenseId, decimal amount)
        {
                await
                    Put(Payment,
                        new Expense()
                        {
                            Amount = amount,
                            ExpenseId = expenseId,
                            ShiftId = shiftId,
                            CoffeeRoomNo = CoffeeRoomNo
                        });
        }

        public async Task<Entity[]> GetExpenseItems()
        {
            return await Get<Entity[]>($"{Payment}/getexpenseitems");
        }

        public async Task AddNewExpenseType(string typeName)
        {
            await Put<object>($"{Payment}/addnewexpensetype", null, new Dictionary<string, string>() { {nameof(typeName), typeName} });
        }
    }
}
