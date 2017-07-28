using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class ProductsCalculationViewModel : ViewModelBase
    {
        private ProductManager manager = new ProductManager();
        private List<ItemViewModel> _items = new List<ItemViewModel>();
        public List<ItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }
        public async void Init()
        {
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var items = await manager.GetProducts();
            Items = items.Select(s => new ItemViewModel(s)).ToList();
        }
    }
}
