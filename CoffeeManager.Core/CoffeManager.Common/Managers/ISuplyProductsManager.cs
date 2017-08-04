using System;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface ISuplyProductsManager
    {
        Task<SupliedProduct[]> GetSuplyProducts();

        Task AddSuplyProduct(string newProduct);

        Task<SupliedProduct> GetSuplyProduct(int id);

        Task EditSuplyProduct(int id, string name, decimal supliedPrice, decimal? itemCount);

        Task DeleteSuplyProduct(int _id);

        Task<ProductCalculationEntity> GetProductCalculationItems(int productId);

        Task DeleteProductCalculationItem(int id);

        Task AddProductCalculationItem(int productId, int id, decimal quantity);
    }
}
