using System;

namespace CoffeeManager.Models.Data.DTO.StaffMotivation
{
    public class ShiftMotivationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShiftId { get; set; }
        public int MotivationId { get; set; }
        public DateTime Date { get; set; }
        public decimal ShiftScore { get; set; }
        public decimal MoneyScore { get; set; }
        public decimal OtherScore { get; set; }    
    }
}