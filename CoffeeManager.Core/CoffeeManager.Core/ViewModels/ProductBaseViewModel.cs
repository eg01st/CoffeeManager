using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.Managers;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public abstract class ProductBaseViewModel : ViewModelBase
    {
        protected ProductManager ProductManager = new ProductManager();

        protected List<ProductViewModel> _items;

        public List<ProductViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        protected abstract Product[] GetProducts();

        public void Init()
        {
            _items = GetProducts().Select(s => new ProductViewModel(s)).ToList();
        }
    }
}
