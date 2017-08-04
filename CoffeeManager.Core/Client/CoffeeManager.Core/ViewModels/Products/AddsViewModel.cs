using System.Threading.Tasks;
using CoffeeManager.Core.ViewModels.Products;
using CoffeeManager.Models;
using CoffeManager.Common;

namespace CoffeeManager.Core.ViewModels
{
    public class AddsViewModel : ProductBaseViewModel
    {
        public AddsViewModel(IProductManager productManager) : base(productManager)
        {
        }

        protected async override Task<Product[]> GetProducts()
        {
            return await ProductManager.GetAddsProducts();
        }
    }
}
