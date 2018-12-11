namespace CoffeeManager.Models.Data.DTO.AutoOrder
{
    public class SuplyProductToOrderItemDTO
    {
        public int Id { get; set; }
        
        public int OrderId { get; set; }
        
        public int SuplyProductId { get; set; }
        
        public string SuplyProductName { get; set; }

        public int QuantityShouldBeAfterOrder { get; set; }
        
        public bool ShouldUpdateQuantityBeforeOrder { get; set; }
    }
}