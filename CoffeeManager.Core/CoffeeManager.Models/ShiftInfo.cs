using System;

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

        public decimal? CreditCardAmount { get; set; }
        public decimal? TotalCreditCardAmount { get; set; }

        public int? StartCounter { get; set; }

        public int? EndCounter { get; set; }
        public decimal UsedPortions { get; set; }

        public bool IsFinished { get; set; }
    }
}
