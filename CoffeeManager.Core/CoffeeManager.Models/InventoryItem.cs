namespace CoffeeManager.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public int SuplyProductId { get; set; }
        public string SuplyProductName { get; set; }
        public decimal QuantityBefore { get; set; }
        public decimal QuantityAfer { get; set; }
        public decimal QuantityDiff { get; set; }
        public int CoffeeRoomNo { get; set; }
    }
}
