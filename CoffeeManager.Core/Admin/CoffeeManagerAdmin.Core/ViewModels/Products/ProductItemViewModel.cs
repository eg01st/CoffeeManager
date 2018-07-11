using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.Product;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace CoffeeManagerAdmin.Core.ViewModels.Products
{
    public class ProductItemViewModel : ListItemViewModelBase
    {
        private ProductDTO prod;
        public ICommand DeleteProductCommand { get; }
        public ICommand ToggleIsActiveCommand {get;}

        public string Category { get; set; }
        public string CupType {get;set;}
        public bool IsActive {get;set;}

        public ProductItemViewModel(ProductDTO prod)
        {
            this.prod = prod;
            Name = prod.Name;
            IsActive = prod.IsActive;
            Category = prod.CategoryName;

            var type = (CupTypeEnum)prod.CupType;
            CupType = type == CupTypeEnum.Unknown ? string.Empty : type.ToString();
            RaiseAllPropertiesChanged();

            DeleteProductCommand = new MvxCommand(DoDeleCommand);
            ToggleIsActiveCommand = new MvxAsyncCommand(DoToggleIsActive);
        }

        private async Task DoToggleIsActive()
        {
            IProductManager manager = Mvx.Resolve<IProductManager>();
            await manager.ToggleIsActiveProduct(prod.Id);
        }

        protected override async void DoGoToDetails()
        {
            await NavigationService.Navigate<ProductDetailsViewModel, ProductDTO>(prod);
        }

        private void DoDeleCommand()
        {
            Confirm($"Действительно удалить продукт \"{prod.Name}\"?", DeleteProduct);
        }

        private async Task DeleteProduct()
        {
            IProductManager manager = Mvx.Resolve<IProductManager>();
            await manager.DeleteProduct(prod.Id);
            Publish(new ProductListChangedMessage(this));
        }
    }
}
