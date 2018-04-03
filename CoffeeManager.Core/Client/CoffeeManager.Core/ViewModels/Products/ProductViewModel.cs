using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.Category;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MobileCore.Extensions;
using MvvmCross.Platform;

namespace CoffeeManager.Core.ViewModels.Products
{
    public class ProductViewModel : ViewModelBase
    {
        private readonly CategoryDTO category;
        private readonly IProductManager productManager;
        private ProductItemViewModel[] items;
        private List<ProductViewModel> subCategories;

        public ProductItemViewModel[] Items
        {
            get => items;
            set
            {
                items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }
        
        public List<ProductViewModel> SubCategories
        {
            get => subCategories;
            set
            {
                subCategories = value;
                RaisePropertyChanged(nameof(SubCategories));
                RaisePropertyChanged(nameof(HasSubCategories));
            }
        }

        public bool HasSubCategories => SubCategories.IsNotNullNorEmpty();

        public int CategoryId => category.Id;

        public string CategoryName => category.Name;

        public ProductViewModel(CategoryDTO category)
        {
            this.category = category;
            this.productManager = Mvx.Resolve<IProductManager>();
        }
        
        public async Task InitViewModel()
        {
            await GetItems();
        }

        private async Task GetItems()
        {
            if (category.SubCategories.IsNotNullNorEmpty())
            {
                var subCategories = new List<ProductViewModel>();
                var tasks = new List<Task>();
                foreach (var subCategory in category.SubCategories)
                {
                    var vm = new ProductViewModel(subCategory);
                    subCategories.Add(vm);
                    tasks.Add(vm.InitViewModel());
                }

                await Task.WhenAll(tasks);
                SubCategories = subCategories;
            }
            else
            {
                var items = await productManager.GetProducts(category.Id);
                Items = items.Select(s => new ProductItemViewModel(s)).ToArray();
            }
        }
    }
}
