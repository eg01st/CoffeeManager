using System.Threading.Tasks;
using CoffeeManager.Core.ViewModels.Products;
using CoffeeManager.Models;
using CoffeManager.Common;

namespace CoffeeManager.Core.ViewModels
{
    public class ColdDrinksViewModel : ProductBaseViewModel
    {
        public ColdDrinksViewModel(IProductManager productManager) : base(productManager)
        {
        }

        protected override async Task<Product[]> GetProducts()
        {
            return await ProductManager.GetColdDrinksProducts();
        }
    }
}
