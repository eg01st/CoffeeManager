using System;
using System.Collections.Generic;

namespace CoffeeManager.Models.Data.DTO.AutoOrder
{
    public class AutoOrderDTO
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan OrderTime { get; set; }
        public bool IsActive { get; set; }
        public int CoffeeRoomId { get; set; }
        
        public List<SuplyProductToOrderItemDTO> OrderItems { get; set; }
    }
}