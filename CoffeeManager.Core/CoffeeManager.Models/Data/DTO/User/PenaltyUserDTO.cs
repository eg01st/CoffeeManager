using System;
namespace CoffeeManager.Models.User
{
    public class PenaltyUserDTO
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
    }
}
