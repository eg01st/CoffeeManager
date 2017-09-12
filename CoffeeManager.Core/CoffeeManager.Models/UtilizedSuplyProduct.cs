using System;

namespace CoffeeManager.Models
{
    public class UtilizedSuplyProduct
    {
        public int Id { get; set; }
        public int SuplyProductId { get; set; }
        public string SuplyProductName { get; set; }
        public int ShiftId { get; set; }
        public decimal Quantity { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public int CoffeeRoomNo { get; set; }
    }
}
