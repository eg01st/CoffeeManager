using System.Collections.Generic;

namespace CoffeeManager.Models
{
    public class SaleStorage
    {
        public List<Sale> Sales { get; set; } = new List<Sale>();
        public List<Sale> DismissedSales { get; set; } = new List<Sale>();

        public List<Sale> UtilizedSales { get; set; } = new List<Sale>();
    }
}
