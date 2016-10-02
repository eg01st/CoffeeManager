using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ServiceProviders
{
    public class ProductProvider : BaseServiceProvider
    {
        private const string Products = "Products";
        public async Task<Product[]> GetProduct(ProductType type)
        {
            return await Get<Product[]>(Products,
                new Dictionary<string, string>()
                {
                    {nameof(ProductType), ((int) type).ToString()},
                });
        }

        public async Task SaleProduct(int shiftId, int id, decimal price, bool isPoliceSale)
        {
            await Put($"{Products}/SaleProduct", new Sale() { ShiftId = shiftId, Amount = price, Product = id, IsPoliceSale = isPoliceSale }, new Dictionary<string, string>() { {nameof(shiftId), shiftId.ToString()} });
        }

        public async Task DeleteSale(int shiftId, int id)
        {
            await Put($"{Products}/DeleteSale", new Sale() { ShiftId = shiftId, Product = id});
        }
    }
}
