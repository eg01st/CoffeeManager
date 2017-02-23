using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManager.Models
{
    public class SaleInfo
    {
        public string Name { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> Producttype { get; set; }
    }
}
