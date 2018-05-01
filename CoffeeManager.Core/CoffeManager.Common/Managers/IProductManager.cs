using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common.Managers
{
    public interface IProductManager
    {
        Task<Product[]> GetProducts(int categoryId);

        Task SaleProduct(int shiftId, int id, decimal price, bool isPoliceSale, bool isCreditCardSale, bool isSaleByWeight, decimal? weight);
        Task DismisSaleProduct(int id);
        Task UtilizeSaleProduct(int id);

        Task AddProduct(Product product);

        Task DeleteProduct(int id);

        Task EditProduct(Product product);

        Task<Product[]> GetProducts();

        Task ToggleIsActiveProduct(int id);

        Task<string[]> GetAvaivalbeProductColors();
    }
}
