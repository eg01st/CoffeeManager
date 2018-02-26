using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeManager.Common;

namespace CoffeeManager.Core.ViewModels.Products
{
    public class CoffeeViewModel : ProductBaseViewModel
    {
        public CoffeeViewModel(IProductManager productManager) : base(productManager)
        {
        }

        protected override async Task<Product[]> GetProducts()
        {
            return await ProductManager.GetCoffeeProducts();
        }
    }
}
