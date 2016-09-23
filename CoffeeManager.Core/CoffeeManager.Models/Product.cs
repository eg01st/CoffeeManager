namespace CoffeeManager.Models
{
    public class Product : Entity
    {
        public float Price { get; set; }
        public bool IsPoliceSale { get; set; }
        public ProductType Type { get; set; }
    }
}
