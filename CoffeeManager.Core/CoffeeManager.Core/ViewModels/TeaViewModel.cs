using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class TeaViewModel : ProductBaseViewModel
    {
        protected override Product[] GetProducts(bool isPoliceSale)
        {
            return ProductManager.GetTeaProducts(isPoliceSale);
        }
    }
}
