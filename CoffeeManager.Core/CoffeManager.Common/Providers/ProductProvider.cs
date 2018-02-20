using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models;
using RestSharp.Portable;

namespace CoffeManager.Common
{
    public class ProductProvider : ServiceBase, IProductProvider
    {
        public async Task AddProduct(Product product)
        {
            var request = CreatePutRequest(RoutesConstants.AddProduct);
            request.AddBody(product);
            await ExecuteRequestAsync(request);
        }

        public async Task DeleteProduct(int id)
        {
            var request = CreateDeleteRequest(RoutesConstants.DeleteProduct);
            request.Parameters.Add(new Parameter() { Name = nameof(id), Value = id });
            await ExecuteRequestAsync(request);
        }

        public async Task EditProduct(Product product)
        {
            var request = CreatePostRequest(RoutesConstants.EditProduct);
            request.AddBody(product);
            await ExecuteRequestAsync(request);
        }

        public async Task<Product[]> GetProducts()
        {
            var request = CreateGetRequest(RoutesConstants.GetAllProducts);
            return await ExecuteRequestAsync<Product[]>(request);
        }

        public async Task ToggleIsActiveProduct(int id)
        {
            var request = CreatePostRequest(RoutesConstants.ToggleProductEnabled);
            request.Parameters.Add(new Parameter() { Name = nameof(id), Value = id });
            await ExecuteRequestAsync(request);
        }

        public async Task<ProductEntity[]> GetProduct(ProductType type)
        {
            var request = CreateGetRequest(RoutesConstants.Products);
            request.Parameters.Add(new Parameter() { Name = nameof(ProductType), Value = type });
            var prods = await ExecuteRequestAsync<ProductEntity[]>(request);
            return prods.Where(p => p.IsActive).ToArray();
        }

        public async Task SaleProduct(Sale sale)
        {
            var request = CreatePutRequest(RoutesConstants.SaleProduct);
            request.AddBody(sale);
            await ExecuteRequestAsync(request);
        }

        public async Task DeleteSale(int shiftId, int id)
        {
            var request = CreatePostRequest(RoutesConstants.DeleteSale);
            request.AddBody(new Sale() { ShiftId = shiftId, Id = id });
            await ExecuteRequestAsync(request);
        }

        public async Task UtilizeSaleProduct(int shiftId, int id)
        {
            var request = CreatePostRequest(RoutesConstants.UtilizeSale);
            request.AddBody(new Sale() { ShiftId = shiftId, Id = id });
            await ExecuteRequestAsync(request);
        }
    }
}
