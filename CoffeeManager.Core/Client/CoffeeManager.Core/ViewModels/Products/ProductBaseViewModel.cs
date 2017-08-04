using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeManager.Common;

namespace CoffeeManager.Core.ViewModels.Products
{
    public abstract class ProductBaseViewModel : ViewModelBase
    {
        protected readonly IProductManager ProductManager;

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

        public ProductBaseViewModel(IProductManager productManager)
        {
            this.ProductManager = productManager;
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
