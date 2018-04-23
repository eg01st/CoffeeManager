using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common.Managers
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

        Task TransferSuplyProducts(IEnumerable<TransferSuplyProductRequest> requests);
    }
}
