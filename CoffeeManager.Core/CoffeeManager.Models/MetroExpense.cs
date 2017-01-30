using System;

namespace CoffeeManager.Models
{
    public class MetroExpense
    {
        public Nullable<int> Productid { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Quantity { get; set; }
    }
}
