using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.Category;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MobileCore.Extensions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace CoffeeManager.Core.ViewModels.Products
{
    public class ProductViewModel : ViewModelBase
    {
        private CategoryDTO category;
        private readonly IProductManager productManager;
        private MvxObservableCollection<ProductViewModel> subCategories;

        public MvxObservableCollection<ProductItemViewModel> Items
        {
            get;
            set;
        } = new MvxObservableCollection<ProductItemViewModel>();
        
        public MvxObservableCollection<ProductViewModel> SubCategories
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

        public ProductViewModel()
        {
            this.productManager = Mvx.Resolve<IProductManager>();
        }
        
        public async Task InitViewModel(CategoryDTO category)
        {
            this.category = category;
            await GetItems();
        }

        private async Task GetItems()
        {
            if (category.SubCategories.IsNotNullNorEmpty())
            {
                var subCats = new List<ProductViewModel>();
                var tasks = new List<Task>();
                foreach (var subCategory in category.SubCategories)
                {
                    var vm = new ProductViewModel();
                    subCats.Add(vm);
                    tasks.Add(vm.InitViewModel(subCategory));
                }

                await Task.WhenAll(tasks);
                SubCategories = new MvxObservableCollection<ProductViewModel>(subCats);
            }
            else
            {
                var products = await productManager.GetProducts(category.Id);
                Items.ReplaceWith(products.Select(s => new ProductItemViewModel(s)));
            }
        }
    }
}
