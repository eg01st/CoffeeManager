using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.Managers;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class CoffeeViewModel : ProductBaseViewModel
    {
        protected override async Task<Product[]> GetProducts(bool isPoliceSale)
        {
            return await ProductManager.GetCoffeeProducts(isPoliceSale);
        }
    }
}
