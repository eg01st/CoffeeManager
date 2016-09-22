using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class ColdDrinksViewModel : ProductBaseViewModel
    {
        protected override Product[] GetProducts(bool isPoliceSale)
        {
            return ProductManager.GetColdDrinksProducts(isPoliceSale);
        }
    }
}
