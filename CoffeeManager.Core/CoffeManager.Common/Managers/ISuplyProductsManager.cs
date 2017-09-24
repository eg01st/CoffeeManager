using System;
using System.Threading.Tasks;
using CoffeeManager.Models;
using System.Collections.Generic;

namespace CoffeManager.Common
{
    public interface ISuplyProductsManager
    {
        Task<SupliedProduct[]> GetSuplyProducts();

        Task AddSuplyProduct(string newProduct);

        Task<SupliedProduct> GetSuplyProduct(int id);

        Task EditSuplyProduct(SupliedProduct prod);

        Task DeleteSuplyProduct(int _id);

        Task<ProductCalculationEntity> GetProductCalculationItems(int productId);

        Task DeleteProductCalculationItem(int id);

        Task AddProductCalculationItem(int productId, int id, decimal quantity);

        Task UtilizeSuplyProduct(UtilizedSuplyProduct product);

        Task<IEnumerable<UtilizedSuplyProduct>> GetUtilizedProducts();
    }
}
