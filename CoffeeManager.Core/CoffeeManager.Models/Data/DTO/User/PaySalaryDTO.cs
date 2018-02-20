using System;
namespace CoffeeManager.Models
{
    public class PaySalaryDTO
    {
        public int UserId { get; set; }
        public int CoffeeRoomIdToPay { get; set; }
    }
}
