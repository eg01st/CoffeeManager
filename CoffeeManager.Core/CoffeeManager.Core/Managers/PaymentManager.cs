using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManager.Core.Managers
{
    public class PaymentManager : BaseManager
    {
        public float GetCurrentShiftMoney()
        {
            return 33.55f;
        }

        public float GetEntireMoney()
        {
            return 5555.44f;
        }

        public void AddExpense(int expenseId, float amout)
        {
            
        }

        public ItemBase[] GetExpenseItems()
        {
            return new ItemBase[]
            {
                new ItemBase { Id = 1, Name = "Кофе"},
                new ItemBase { Id = 1, Name = "Молоко"},
                new ItemBase { Id = 1, Name = "Панини"},
            };
        }

        public void AddNewExpenseType(string typeName)
        {
            
        }
    }
}
