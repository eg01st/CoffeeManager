using System.Threading.Tasks;
using CoffeeManager.Core.ViewModels.Products;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class SweetsViewModel : ProductBaseViewModel
    {
        protected override async Task<Product[]> GetProducts()
        {
            return await ProductManager.GetSweetsProducts();
        }
    }
}
