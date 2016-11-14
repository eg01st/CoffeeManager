using System;
using System.Collections.Generic;

namespace CoffeeManager.Models
{
    public class Order : Entity
    {
        public DateTime Date { get; set; }
        public bool IsDone { get; set; }
        public decimal Price { get; set; }
        public int? ExpenseTypeId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
