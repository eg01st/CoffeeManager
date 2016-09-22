namespace CoffeeManager.Models
{
    public class Product : ItemBase
    {
        public float Price { get; set; }
        public bool IsPoliceSale { get; set; }
    }
}
