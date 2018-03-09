using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common.Managers
{
    public interface IProductManager
    {
        Task<Product[]> GetProducts(int categoryId);
        
//        Task<Product[]> GetCoffeeProducts();
//
//        Task<Product[]> GetTeaProducts();
//
//        Task<Product[]> GetColdDrinksProducts();
//
//        Task<Product[]> GetIceCreamProducts();
//
//        Task<Product[]> GetMealsProducts();
//
//        Task<Product[]> GetWaterProducts();
//
//        Task<Product[]> GetSweetsProducts();
//
//        Task<Product[]> GetAddsProducts();

        Task SaleProduct(int shiftId, int id, decimal price, bool isPoliceSale, bool isCreditCardSale, bool isSaleByWeight, decimal? weight);
        Task DismisSaleProduct(int id);
        Task UtilizeSaleProduct(int id);

        Task AddProduct(string name, string price, string policePrice, int cupType, int productTypeId, bool isSaleByWeight);

        Task DeleteProduct(int id);

        Task EditProduct(int id, string name, string price, string policePrice, int cupType, int productTypeId, bool isSaleByWeight);

        Task<Product[]> GetProducts();

        Task ToggleIsActiveProduct(int id);
    }
}
