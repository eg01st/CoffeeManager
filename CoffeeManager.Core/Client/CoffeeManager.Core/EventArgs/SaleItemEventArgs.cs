using System;
namespace CoffeeManager.Core
{
    public class SaleItemEventArgs : EventArgs
    {
        public decimal Price {get;set;}
        public decimal? Weight {get;set;}
        public bool IsSaleByWeight {get;set;}

        public SaleItemEventArgs(decimal price, decimal? weight, bool isSaleByWeight)
        {
            Price = price;
            Weight = weight;
            IsSaleByWeight = isSaleByWeight;
        }
    }
}
