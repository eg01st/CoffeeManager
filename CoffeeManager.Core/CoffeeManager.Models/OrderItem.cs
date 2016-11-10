namespace CoffeeManager.Models
{
    public class OrderItem : Entity
    {
        public bool IsDone { get;  set; }
        public int? OrderId { get;  set; }
        public decimal Price { get;  set; }
        public decimal Quantity { get;  set; }
    }
}