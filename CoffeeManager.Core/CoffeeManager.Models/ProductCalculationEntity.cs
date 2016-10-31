namespace CoffeeManager.Models
{
    public class ProductCalculationEntity : Entity
    {
        public int ProductId { get; set; }

        public CalculationItem[] SuplyProductInfo { get; set; }
    }
}
