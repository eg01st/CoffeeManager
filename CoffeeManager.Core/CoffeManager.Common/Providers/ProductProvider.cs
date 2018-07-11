using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.Product;
using CoffeManager.Common.Database;

namespace CoffeManager.Common.Providers
{
    public class ProductProvider : BaseServiceProvider, IProductProvider
    {
        public async Task AddProduct(ProductDTO productDTO)
        {
            await Put(RoutesConstants.AddProduct, productDTO);
        }

        public async Task DeleteProduct(int id)
        {
            await Delete(RoutesConstants.DeleteProduct, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task EditProduct(ProductDTO productDTO)
        {
            await Post(RoutesConstants.EditProduct, productDTO);
        }

        public async Task<ProductDTO[]> GetProducts()
        {
            return await Get<ProductDTO[]>(RoutesConstants.GetAllProducts);
        }

        public async Task ToggleIsActiveProduct(int id)
        {
            await Post<Object>(RoutesConstants.ToggleProductEnabled, null, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task<ProductEntity[]> GetProduct(int categoryId)
        {
            var prods = await Get<ProductEntity[]>(RoutesConstants.Products,
                new Dictionary<string, string>()
                {
                    {nameof(ProductType), categoryId.ToString()},
                });
            return prods.Where(p => p.IsActive).ToArray();
        }

        public async Task SaleProduct(Sale sale)
        {
            await Put(RoutesConstants.SaleProduct, sale);
        }

        public async Task DeleteSale(int shiftId, int id)
        {
            await Post(RoutesConstants.DeleteSale, new Sale() { ShiftId = shiftId, Id = id });
        }

        public async Task UtilizeSaleProduct(int shiftId, int id)
        {
            await Post(RoutesConstants.UtilizeSale, new Sale() { ShiftId = shiftId, Id = id });
        }
        
        public async Task<string[]> GetAvaivalbeProductColors()
        {
            return await Get<string[]>(RoutesConstants.GetAvaivalbeProductColors);
        }
    }
}
