using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ServiceProviders;
namespace CoffeeManagerAdmin.Core
{
    public class ProductProvider : BaseServiceProvider
    {
        public ProductProvider()
        {
        }

        public async Task AddProduct(Product product)
        {
            await Put("products/addproduct", product);
        }


        public async Task DeleteProduct(int id)
        {
            await Delete("products/deleteproduct", new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task EditProduct(Product product)
        {
            await Post("products/editproduct", product);
        }

        public async Task<Product[]> GetProducts()
        {
            return await Get<Product[]>("products/getAll");
        }

        public async Task ToggleIsActiveProduct(int id)
        {
            await Post<Object>("products/toggleProductEnabled", null, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }
    }
}
