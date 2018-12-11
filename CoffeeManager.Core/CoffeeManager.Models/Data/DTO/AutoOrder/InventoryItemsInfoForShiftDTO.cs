using System.Collections.Generic;

namespace CoffeeManager.Models.Data.DTO.AutoOrder
{
    public class InventoryItemsInfoForShiftDTO
    {
        public List<SupliedProduct> Items { get; set; }
        public int AutoOrderId { get; set; }
    }
}