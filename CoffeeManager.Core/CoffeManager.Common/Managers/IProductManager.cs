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

        Task AddProduct(string name, string price, string policePrice, int cupType, int productTypeId, bool isSaleByWeight, int categoryId);

        Task DeleteProduct(int id);

        Task EditProduct(int id, string name, string price, string policePrice, int cupType, int productTypeId, bool isSaleByWeight, int categoryId);

        Task<Product[]> GetProducts();

        Task ToggleIsActiveProduct(int id);

        Task<string[]> GetAvaivalbeProductColors();
    }
}
