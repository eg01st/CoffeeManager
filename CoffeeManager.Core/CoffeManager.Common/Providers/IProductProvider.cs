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

        Task<ProductEntity[]> GetProduct(ProductType type);

        Task SaleProduct(Sale sale);

        Task DeleteSale(int shiftId, int id);

        Task UtilizeSaleProduct(int shiftId, int id);
    }
}
