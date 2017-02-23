using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.ViewModels.Products;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class ColdDrinksViewModel : ProductBaseViewModel
    {
        protected override async Task<Product[]> GetProducts()
        {
            return await ProductManager.GetColdDrinksProducts();
        }
    }
}
