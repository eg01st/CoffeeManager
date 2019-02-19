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
        public string Subject { get; set; }
        public string EmailToSend { get; set; }
        public string CCToSend { get; set; }
        public string SenderEmail { get; set; }
        public string SenderEmailPassword { get; set; }
        
        public List<SuplyProductToOrderItemDTO> OrderItems { get; set; }
    }
}