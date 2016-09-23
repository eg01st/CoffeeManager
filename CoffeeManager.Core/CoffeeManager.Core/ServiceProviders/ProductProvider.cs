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
        public async Task<Product[]> GetProduct(ProductType type, bool isPoliceSale)
        {
            return await Get<Product[]>(Products,
                new Dictionary<string, string>()
                {
                    {nameof(ProductType), ((int) type).ToString()},
                    {nameof(isPoliceSale), isPoliceSale.ToString()}
                });
        }

        public async Task SaleProduct(Product product)
        {
            await Put($"{Products}/SaleProduct", product);
        }

        public async Task DeleteSale(Product product)
        {
            await Put($"{Products}/DeleteSale", product);
        }
    }
}
