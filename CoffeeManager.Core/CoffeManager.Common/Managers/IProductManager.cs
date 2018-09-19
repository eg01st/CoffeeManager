using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.Product;

namespace CoffeManager.Common.Managers
{
    public interface IProductManager
    {
        Task<ProductDTO[]> GetProducts(int categoryId);

        Task SaleProduct(int shiftId, int id, decimal price, bool isPoliceSale, bool isCreditCardSale, bool isSaleByWeight, decimal? weight);
        Task DismisSaleProduct(int id);
        Task UtilizeSaleProduct(int id);

        Task<int> AddProduct(ProductDetaisDTO productDTO);

        Task DeleteProduct(int id);

        Task EditProduct(ProductDetaisDTO productDTO);

        Task<ProductDTO[]> GetProducts();

        Task ToggleIsActiveProduct(int id);

        Task<string[]> GetAvaivalbeProductColors();
        
        Task<ProductDetaisDTO> GetProduct(int productId);
    }
}
