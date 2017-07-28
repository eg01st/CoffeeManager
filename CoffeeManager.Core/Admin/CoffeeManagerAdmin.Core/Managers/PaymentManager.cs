using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ServiceProviders;

namespace CoffeeManagerAdmin.Core.Managers
{
    public class PaymentManager : BaseManager
    {
        PaymentProvider provider = new PaymentProvider();
        public async Task<Expense[]> GetShiftExpenses(int id)
        {
            return await provider.GetShiftExpenses(id);
        }

        public async Task<ExpenseType[]> GetExpenseItems()
        {
            return await provider.GetExpenseItems();
        }

        public async Task ToggleIsActiveExpense(int id)
        {
            await provider.ToggleIsActiveExpense(id);
        }
   }
}
