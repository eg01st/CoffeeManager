using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class MealsViewModel : ProductBaseViewModel
    {
        protected override Product[] GetProducts()
        {
            return ProductManager.GetMealsProducts();
        }
    }
}
