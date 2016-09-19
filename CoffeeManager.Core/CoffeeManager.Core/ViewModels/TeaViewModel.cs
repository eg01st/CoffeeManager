using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class TeaViewModel : ProductBaseViewModel
    {
        protected override Product[] GetProducts()
        {
            return ProductManager.GetTeaProducts();
        }
    }
}
