using System;

namespace CoffeeManager.Models.Data.DTO.StaffMotivation
{
    public class MotivationDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}