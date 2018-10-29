namespace CoffeeManager.Models.Data.DTO.AutoOrder.History
{
    public class SuplyProductOrderItemDTO
    {
        public int Id { get; set; }
        public int SuplyProductId { get; set; }
        public string SuplyProductName { get; set; }
        public int QuantityBefore { get; set; }
        public int OrderedQuantity { get; set; }
    }
}