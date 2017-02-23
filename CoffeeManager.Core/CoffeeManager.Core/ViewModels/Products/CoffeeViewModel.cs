using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.Managers;
using CoffeeManager.Core.ViewModels.Products;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class CoffeeViewModel : ProductBaseViewModel
    {
        protected override async Task<Product[]> GetProducts()
        {
            return await ProductManager.GetCoffeeProducts();
        }
    }
}
