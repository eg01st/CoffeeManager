using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private Product product;

        public int Id => product.Id;

        public float Price => product.Price;

        public string Name => product.Name;

        public ProductViewModel(Product product)
        {
            this.product = product;
        }

    }
}
