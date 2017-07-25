﻿using System;
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

        public async Task AddExpense(int shiftId, int expenseId, decimal amount, int itemCount)
        {
                await
                    Put(Payment,
                        new Expense()
                        {
                            Amount = amount,
                            ItemCount = itemCount,
                            ExpenseId = expenseId,
                            ShiftId = shiftId,
                            CoffeeRoomNo = CoffeeRoomNo
                        });
        }

        public async Task<ExpenseType[]> GetExpenseItems()
        {
            return await Get<ExpenseType[]>($"{Payment}/getexpenseitems");
        }

        public async Task AddNewExpenseType(string typeName)
        {
            await Put<object>($"{Payment}/addnewexpensetype", null, new Dictionary<string, string>() { {nameof(typeName), typeName} });
        }

        public async Task<Expense[]> GetShiftExpenses(int id)
        {
            return
                await
                    Get<Expense[]>($"{Payment}/getShiftExpenses",
                        new Dictionary<string, string>() {{nameof(id), id.ToString()}});
        }

        public async Task DeleteExpenseItem(int id)
        {
            await Delete($"{Payment}/deleteexpense", new Dictionary<string, string>() {{nameof(id), id.ToString()}});
        }
    }
}
