using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManager.Core.ViewModels.Products
{
    public abstract class ProductBaseViewModel : ViewModelBase
    {
        private readonly MvxSubscriptionToken token;

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

        protected abstract Task<Product[]> GetProducts();


        public async void Init()
        {
            await GetItems();
        }

        private async Task GetItems()
        {
            var items = await GetProducts();
            Items = items.Select(s => new ProductViewModel(s)).ToList();
        }
    }
}
