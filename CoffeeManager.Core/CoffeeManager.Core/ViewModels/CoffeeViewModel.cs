using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.Managers;

namespace CoffeeManager.Core.ViewModels
{
    public class CoffeeViewModel : ViewModelBase
    {

        private ProductManager _productManager = new ProductManager();

        private List<ProductViewModel> _items;

        public List<ProductViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        } 

        public CoffeeViewModel()
        {
            _items = _productManager.GetCoffeeProducts().Select(s => new ProductViewModel(s)).ToList();
        }
    }
}
