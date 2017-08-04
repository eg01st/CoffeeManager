using System;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface IProductProvider
    {
        Task AddProduct(Product product);

        Task DeleteProduct(int id);

        Task EditProduct(Product product);

        Task<Product[]> GetProducts();

        Task ToggleIsActiveProduct(int id);

        Task<Product[]> GetProduct(ProductType type);

        Task SaleProduct(int shiftId, int id, decimal price, bool isPoliceSale, bool isCreditCardSale, bool isSaleByWeight, decimal? weight);

        Task DeleteSale(int shiftId, int id);

        Task UtilizeSaleProduct(int shiftId, int id);
    }
}
