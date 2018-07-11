using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.Product;
using CoffeManager.Common.Database;

namespace CoffeManager.Common.Providers
{
    public interface IProductProvider
    {
        Task AddProduct(ProductDTO productDTO);

        Task DeleteProduct(int id);

        Task EditProduct(ProductDTO productDTO);

        Task<ProductDTO[]> GetProducts();

        Task ToggleIsActiveProduct(int id);

        Task<ProductEntity[]> GetProduct(int categoryId);

        Task SaleProduct(Sale sale);

        Task DeleteSale(int shiftId, int id);

        Task UtilizeSaleProduct(int shiftId, int id);

        Task<string[]> GetAvaivalbeProductColors();
    }
}
