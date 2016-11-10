using System;

namespace CoffeeManager.Models
{
    public class Order : Entity
    {
        public DateTime Date { get; set; }
        public bool IsDone { get; set; }
        public decimal Price { get; set; }
    }
}
