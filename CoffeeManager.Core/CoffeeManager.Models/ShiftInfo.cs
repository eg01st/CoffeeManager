using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManager.Models
{
    public class ShiftInfo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public decimal ShiftEarnedMoney { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal StartMoney { get; set; }
        public decimal RealAmount { get; set; }
        public decimal ExpenseAmount { get; set; }


    }
}
