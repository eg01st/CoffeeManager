using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManager.Models
{
    public class Expense : Entity
    {
        public int ShiftId { get; set; }
        public int ExpenseId { get; set; }
        public decimal Amount { get; set; }

        public int ItemCount { get; set; }
    }
}
