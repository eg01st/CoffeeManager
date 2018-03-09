using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Platform;

namespace CoffeeManager.Core.ViewModels.Products
{
    public class ProductViewModel : ViewModelBase
    {
        private readonly int categoryId;
        private readonly IProductManager productManager;
        private ProductItemViewModel[] items;

        public ProductItemViewModel[] Items
        {
            get { return items; }
            set
            {
                items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        public int CategoryId => categoryId;

        public ProductViewModel(int categoryId)
        {
            this.categoryId = categoryId;
            this.productManager = Mvx.Resolve<IProductManager>();
        }
        
        public async Task InitViewModel()
        {
            await GetItems();
        }

        private async Task GetItems()
        {
            var items = await productManager.GetProducts(categoryId);
            Items = items.Select(s => new ProductItemViewModel(s)).ToArray();
        }
    }
}
