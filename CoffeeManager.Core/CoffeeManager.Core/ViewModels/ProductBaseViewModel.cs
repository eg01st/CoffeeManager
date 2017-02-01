using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.Managers;
using CoffeeManager.Core.Messages;
using CoffeeManager.Models;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManager.Core.ViewModels
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
            try
            {
                var items = await GetProducts();
                Items = items.Select(s => new ProductViewModel(s)).ToList();
            }
            catch (ArgumentNullException ex)
            {

            }

        }
    }
}
