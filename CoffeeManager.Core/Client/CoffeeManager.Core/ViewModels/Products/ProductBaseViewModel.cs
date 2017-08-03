using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Core.Managers;
using CoffeeManager.Models;
using CoffeManager.Common;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManager.Core.ViewModels.Products
{
    public abstract class ProductBaseViewModel : ViewModelBase
    {
        protected ProductManager ProductManager = new ProductManager();

        protected ProductItemViewModel[] _items;

        public ProductItemViewModel[] Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        protected abstract Task<Product[]> GetProducts();


        public async Task InitViewModel()
        {
            await GetItems();
        }

        private async Task GetItems()
        {
            var items = await GetProducts();
            Items = items.Select(s => new ProductItemViewModel(s)).ToArray();
        }
    }
}
