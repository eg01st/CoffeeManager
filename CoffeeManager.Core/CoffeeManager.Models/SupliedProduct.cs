namespace CoffeeManager.Models
{
    public class SupliedProduct : Entity
    {
        public decimal Price { get; set; }
        public decimal Quatity { get; set; }
        public decimal? SalePrice { get; set; }
    }
}
