using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;
using RestSharp.Portable;

namespace CoffeManager.Common
{
    public class SuplyProductsProvider : ServiceBase, ISuplyProductsProvider
    {
        public async Task<SupliedProduct[]> GetSuplyProducts()
        {
            var request = CreateGetRequest(RoutesConstants.GetSuplyProducts);
            return await ExecuteRequestAsync<SupliedProduct[]>(request);
        }

        public async Task<SupliedProduct> GetSuplyProduct(int id)
        {
            var request = CreateGetRequest(RoutesConstants.GetSuplyProduct);
            request.Parameters.Add(new Parameter(){ Name = nameof(id), Value = id});
            return await ExecuteRequestAsync<SupliedProduct>(request);
        }

        public async Task EditSuplyProduct(SupliedProduct supliedProduct)
        {
            var request = CreatePostRequest(RoutesConstants.EditSuplyProduct);
            request.AddBody(supliedProduct);
            await ExecuteRequestAsync(request);
        }

        public async Task AddSuplyProduct(string newProduct)
        {
            var request = CreatePutRequest(RoutesConstants.AddSuplyProduct);
            request.AddBody(new SupliedProduct() 
            { 
                CoffeeRoomNo = Config.CoffeeRoomNo,
                Quatity = 0,
                Price = 0,
                Name = newProduct
            });
            await ExecuteRequestAsync<SupliedProduct>(request);
        }

        public async Task DeleteSuplyProduct(int id)
        {
            var request = CreateDeleteRequest(RoutesConstants.DeleteSuplyProduct);
            request.Parameters.Add(new Parameter() { Name = nameof(id), Value = id });
            await ExecuteRequestAsync<SupliedProduct>(request);
        }

        public async Task<ProductCalculationEntity> GetProductCalculationItems(int productId)
        {
            var request = CreateGetRequest(RoutesConstants.GetProductCalculationItems);
            request.Parameters.Add(new Parameter() { Name = nameof(productId), Value = productId });
            return await ExecuteRequestAsync<ProductCalculationEntity>(request);
        }

        public async Task DeleteProductCalculationItem(int id)
        {
            var request = CreateDeleteRequest(RoutesConstants.DeleteProductCalculationItem);
            request.Parameters.Add(new Parameter() { Name = nameof(id), Value = id });
            await ExecuteRequestAsync<SupliedProduct>(request);
        }

        public async Task AddProductCalculationItem(ProductCalculationEntity productCalculationEntity)
        {
            var request = CreatePutRequest(RoutesConstants.AddProductCalculationItem);
            request.AddBody(productCalculationEntity);
            await ExecuteRequestAsync(request);
        }

        public async Task UtilizeSuplyProduct(UtilizedSuplyProduct product)
        {
            var request = CreatePostRequest(RoutesConstants.UtilizeSuplyProduct);
            request.AddBody(product);
            await ExecuteRequestAsync(request);
        }

        public async Task<IEnumerable<UtilizedSuplyProduct>> GetUtilizedProducts()
        {
            var request = CreateGetRequest(RoutesConstants.GetUtilizedSuplyProducts);
            return await ExecuteRequestAsync<IEnumerable<UtilizedSuplyProduct>>(request);
        }

        public async Task TransferSuplyProducts(IEnumerable<TransferSuplyProductRequest> requests)
        {
            var request = CreatePostRequest(RoutesConstants.TransferSuplyProduct);
            request.AddBody(requests);
            await ExecuteRequestAsync(request);
        }
    }
}
