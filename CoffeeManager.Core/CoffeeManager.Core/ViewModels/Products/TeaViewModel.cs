using System.Threading.Tasks;
using CoffeeManager.Core.ViewModels.Products;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class TeaViewModel : ProductBaseViewModel
    {
        protected override async Task<Product[]> GetProducts()
        {
            return await ProductManager.GetTeaProducts();
        }
    }
}
