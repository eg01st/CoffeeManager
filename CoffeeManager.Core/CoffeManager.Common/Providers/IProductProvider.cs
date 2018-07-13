using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.Product;
using CoffeManager.Common.Database;

namespace CoffeManager.Common.Providers
{
    public interface IProductProvider
    {
        Task AddProduct(ProductDetaisDTO productDTO);

        Task DeleteProduct(int id);

        Task EditProduct(ProductDetaisDTO productDTO);

        Task<ProductDTO[]> GetProducts();

        Task<ProductDetaisDTO> GetProduct(int productId);
        
        Task ToggleIsActiveProduct(int id);

        Task<ProductEntity[]> GetProducts(int categoryId);

        Task SaleProduct(Sale sale);

        Task DeleteSale(int shiftId, int id);

        Task UtilizeSaleProduct(int shiftId, int id);

        Task<string[]> GetAvaivalbeProductColors();
    }
}
