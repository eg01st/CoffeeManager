using System;
using System.Collections.Generic;

namespace CoffeeManager.Models.Data.DTO.AutoOrder.History
{
    public class OrderHistoryItemDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CoffeeRoomId { get; set; }
        
        public List<SuplyProductOrderItemDTO> OrderedItems { get; set; }
    }
}