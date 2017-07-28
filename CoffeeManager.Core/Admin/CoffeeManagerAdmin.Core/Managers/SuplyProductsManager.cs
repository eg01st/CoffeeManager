using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManagerAdmin.Core
{
    public class SuplyProductsManager
    {
        private SuplyProductsServiceProvider provider = new SuplyProductsServiceProvider();

        public SuplyProductsManager()
        {
        }

        public async Task<SupliedProduct[]> GetSupliedProducts()
        {
            return await provider.GetSupliedProducts();
        }

        public async Task AddNewProduct(string newProduct)
        {
            await provider.AddNewProduct(newProduct);
        }

        public async Task DeleteSuplyRequest(int id)
        {
            await provider.DeleteSuplyRequest(id);
        }

        public async Task AddNewSuplyRequest(IEnumerable<SupliedProduct> items)
        {
            await provider.AddNewSuplyRequest(items);
        }

        public async Task<SupliedProduct[]> GetSuplyRequests()
        {
            return await provider.GetSuplyRequests();
        }

        public async Task ProcessSuplyRequests(IEnumerable<SupliedProduct> items)
        {
            await provider.ProcessSuplyRequests(items);
        }

        public async Task<SupliedProduct> GetSupliedProduct(int id)
        {
            return await provider.GetSupliedProduct(id);
        }

        public async Task SaveSuplyProduct(int id, string name, decimal supliedPrice, decimal? itemCount)
        {
            await provider.SaveSuplyProduct(new SupliedProduct { Id = id, Quatity = itemCount, Name = name, Price = supliedPrice, CoffeeRoomNo = Config.CoffeeRoomNo });
        }

        public async Task DeleteSuplyProduct(int _id)
        {
            await provider.DeleteSuplyProduct(_id);
        }

        public async Task<ProductCalculationEntity> GetProductCalculationItems(int productId)
        {
            return await provider.GetProductCalculationItems(productId);
        }

        public async Task DeleteProductCalculationItem(int id)
        {
            await provider.DeleteProductCalculationItem(id);
        }

        public async Task AddProductCalculationItem(int productId, int id, decimal quantity)
        {
            await
                provider.AddProductCalculationItem(new ProductCalculationEntity()
                {
                    ProductId = productId,
                    CoffeeRoomNo = Config.CoffeeRoomNo,
                    SuplyProductInfo =
                        new[]
                        {
                            new CalculationItem()
                            {
                                CoffeeRoomNo = Config.CoffeeRoomNo,
                                SuplyProductId = id,
                                Quantity = quantity
                            },
                        }
                });
        }
    }
}
