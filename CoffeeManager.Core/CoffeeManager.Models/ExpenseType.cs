﻿namespace CoffeeManager.Models
{
    public class ExpenseType : Entity
    {
        public bool IsActive { get; set; }

        public SupliedProduct[] SuplyProducts { get; set; }
    }
}
