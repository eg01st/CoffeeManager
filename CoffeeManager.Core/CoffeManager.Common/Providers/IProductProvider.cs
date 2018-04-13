using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeManager.Common.Database;

namespace CoffeManager.Common.Providers
{
    public interface IProductProvider
    {
        Task AddProduct(Product product);

        Task DeleteProduct(int id);

        Task EditProduct(Product product);

        Task<Product[]> GetProducts();

        Task ToggleIsActiveProduct(int id);

        Task<ProductEntity[]> GetProduct(int categoryId);

        Task SaleProduct(Sale sale);

        Task DeleteSale(int shiftId, int id);

        Task UtilizeSaleProduct(int shiftId, int id);
    }
}
