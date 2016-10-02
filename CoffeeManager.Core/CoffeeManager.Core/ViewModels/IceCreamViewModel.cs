using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class IceCreamViewModel : ProductBaseViewModel
    {
        protected override async Task<Product[]> GetProducts()
        {
            return await ProductManager.GetIceCreamProducts();
        }
    }
}
