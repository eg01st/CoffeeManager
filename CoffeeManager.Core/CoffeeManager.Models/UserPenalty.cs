using System;

namespace CoffeeManager.Models
{
    public class UserPenalty 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
    }
}
