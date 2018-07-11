using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.Product;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;

namespace CoffeeManager.Core.ViewModels.Products
{
    public class ProductBaseViewModel : ViewModelBase
    {
        protected IProductManager ProductManager;

        protected ProductBaseViewModel(IProductManager productManager)
        {
            this.ProductManager = productManager;
        }

        protected virtual Task<ProductDTO[]> GetProducts()
        {
            return
                null;
        }
    }
}