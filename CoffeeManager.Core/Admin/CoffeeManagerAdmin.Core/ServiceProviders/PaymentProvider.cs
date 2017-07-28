using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManagerAdmin.Core.ServiceProviders
{
    public class PaymentProvider : BaseServiceProvider
    {
        public async Task<Expense[]> GetShiftExpenses(int id)
        {
            return await Get<Expense[]>("payment/getShiftExpenses", new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task<ExpenseType[]> GetExpenseItems()
        {
            return await Get<ExpenseType[]>($"payment/getexpenseitems");
        }

        public async Task ToggleIsActiveExpense(int id)
        {
            await Post<object>("payment/toggleExpenseEnabled", null, new Dictionary<string, string>()
            {
                { nameof(id), id.ToString()}
            });
        }
   }
}
