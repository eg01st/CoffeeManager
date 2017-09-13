using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public class SuplyProductsManager : BaseManager, ISuplyProductsManager
    {
        private readonly ISuplyProductsProvider provider;

        public SuplyProductsManager(ISuplyProductsProvider provider)
        {
            this.provider = provider;
        }

        public async Task<SupliedProduct[]> GetSuplyProducts()
        {
            return await provider.GetSuplyProducts();
        }

        public async Task AddSuplyProduct(string newProduct)
        {
            await provider.AddSuplyProduct(newProduct);
        }

        public async Task<SupliedProduct> GetSuplyProduct(int id)
        {
            return await provider.GetSuplyProduct(id);
        }

        public async Task EditSuplyProduct(int id, string name, decimal supliedPrice, decimal? itemCount)
        {
            await provider.EditSuplyProduct(new SupliedProduct { Id = id, Quatity = itemCount, Name = name, Price = supliedPrice, CoffeeRoomNo = Config.CoffeeRoomNo });
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

        public async Task<IEnumerable<UtilizedSuplyProduct>> GetUtilizedProducts()
        {
            return await provider.GetUtilizedProducts();
        }

        public async Task UtilizeSuplyProduct(UtilizedSuplyProduct product)
        {
            await provider.UtilizeSuplyProduct(product);
        }

    }
}
