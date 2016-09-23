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

        protected abstract Task<Product[]> GetProducts(bool isPoliceSale);

        public ProductBaseViewModel()
        {
            token = Subscribe<IsPoliceSaleMessage>(
               async (arg) =>  await GetItems(arg.Data)
            );
        }

        public async void Init()
        {
            await GetItems(false);
        }

        private async Task GetItems(bool isPoliceSale)
        {
            var items = await GetProducts(isPoliceSale);
            Items = items.Select(s => new ProductViewModel(s)).ToList();
        }
    }
}
