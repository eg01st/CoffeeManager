using System.Collections.Generic;

namespace CoffeeManager.Models.Data.Product
{
    public class ProductDetaisDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public decimal PolicePrice { get; set; }
        public int CupType { get; set; }
        public int CoffeeRoomNo { get; set; }
        public int? SuplyId { get; set; }
        public bool IsActive { get; set; }
        public bool IsSaleByWeight { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public bool IsPercentPaymentEnabled { get; set; }
        public List<ProductPaymentStrategyDTO> ProductPaymentStrategies { get; set; }
    }
}