using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface ISuplyProductsProvider
    {
        Task<SupliedProduct[]> GetSuplyProducts();

        Task<SupliedProduct[]> GetSuplyProducts(int coffeeRoomId);

        Task<SupliedProduct> GetSuplyProduct(int id);

        Task EditSuplyProduct(SupliedProduct supliedProduct);

        Task AddSuplyProduct(string newProduct);

        Task DeleteSuplyProduct(int id);

        Task<ProductCalculationEntity> GetProductCalculationItems(int productId);

        Task DeleteProductCalculationItem(int id);

        Task AddProductCalculationItem(ProductCalculationEntity productCalculationEntity);

        Task UtilizeSuplyProduct(UtilizedSuplyProduct product);

        Task<IEnumerable<UtilizedSuplyProduct>> GetUtilizedProducts();

        Task TransferSuplyProducts(IEnumerable<TransferSuplyProductRequest> requests);
    }
}
