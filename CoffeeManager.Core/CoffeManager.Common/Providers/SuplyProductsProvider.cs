using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public class SuplyProductsProvider : BaseServiceProvider, ISuplyProductsProvider
    {
        public async Task<SupliedProduct[]> GetSuplyProducts(int coffeeRoomId)
        {
            var currentCoffeeRoom = Config.CoffeeRoomNo;
            Config.CoffeeRoomNo = coffeeRoomId;

            var res = await Get<SupliedProduct[]>(RoutesConstants.GetSuplyProducts);
            Config.CoffeeRoomNo = currentCoffeeRoom;

            return res;
        }

        public async Task<SupliedProduct[]> GetSuplyProducts()
        {
            return await Get<SupliedProduct[]>(RoutesConstants.GetSuplyProducts);
        }

        public async Task<SupliedProduct> GetSuplyProduct(int id)
        {
            return await Get<SupliedProduct>(RoutesConstants.GetSuplyProduct, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task EditSuplyProduct(SupliedProduct supliedProduct)
        {
            await Post(RoutesConstants.EditSuplyProduct, supliedProduct);
        }

        public async Task AddSuplyProduct(string newProduct)
        {
            await
            Put(RoutesConstants.AddSuplyProduct,
                    new SupliedProduct() { CoffeeRoomNo = Config.CoffeeRoomNo, Quatity = 0, Price = 0, Name = newProduct });
        }

        public async Task DeleteSuplyProduct(int id)
        {
            await Delete(RoutesConstants.DeleteSuplyProduct, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task<ProductCalculationEntity> GetProductCalculationItems(int productId)
        {
            return await
                Get<ProductCalculationEntity>(RoutesConstants.GetProductCalculationItems,
                    new Dictionary<string, string>() { { nameof(productId), productId.ToString() } });
        }

        public async Task DeleteProductCalculationItem(int id)
        {
            await
            Delete(RoutesConstants.DeleteProductCalculationItem,
                    new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task AddProductCalculationItem(ProductCalculationEntity productCalculationEntity)
        {
            await Put(RoutesConstants.AddProductCalculationItem, productCalculationEntity);
        }

        public async Task UtilizeSuplyProduct(UtilizedSuplyProduct product)
        {
            await Post(RoutesConstants.UtilizeSuplyProduct, product);
        }

        public async Task<IEnumerable<UtilizedSuplyProduct>> GetUtilizedProducts()
        {
            return await Get<IEnumerable<UtilizedSuplyProduct>>(RoutesConstants.GetUtilizedSuplyProducts);
        }

        public async Task TransferSuplyProducts(IEnumerable<TransferSuplyProductRequest> requests)
        {
            await Post(RoutesConstants.TransferSuplyProduct, requests);
        }
    }
}
