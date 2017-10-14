using System;
using CoffeeManager.Models;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeeManagerAdmin.Core.ViewModels;
using System.Linq;
using CoffeeManagerAdmin.Core.Util;
using CoffeManager.Common;
using MvvmCross.Platform;
using System.Threading.Tasks;

namespace CoffeeManagerAdmin.Core
{
    public class ProductItemViewModel : ListItemViewModelBase
    {

        private Product _prod;
        private ICommand _deleteProductCommand;
        public ICommand DeleteProductCommand => _deleteProductCommand;
        public ICommand ToggleIsActiveCommand {get;set;}

        public string Category { get; set; }
        public string CupType {get;set;}
        public bool IsActive {get;set;}

        public ProductItemViewModel(Product prod)
        {
            _prod = prod;
            Name = prod.Name;
            IsActive = prod.IsActive;

            var category = TypesLists.ProductTypesList.First(i => i.Id == prod.ProductType);
            Category = category.Name;


            var type = (CupTypeEnum)prod.CupType;
            CupType = type == CupTypeEnum.Unknown ? string.Empty : type.ToString();
            RaiseAllPropertiesChanged();

            _deleteProductCommand = new MvxCommand(DoDeleCommand);
            ToggleIsActiveCommand = new MvxCommand(DoToggleIsActive);
        }

        private async void DoToggleIsActive()
        {
            IProductManager manager = Mvx.Resolve<IProductManager>();
            await manager.ToggleIsActiveProduct(_prod.Id);
        }

        protected override void DoGoToDetails()
        {
            var id = ParameterTransmitter.PutParameter(_prod);
            ShowViewModel<ProductDetailsViewModel>(new {id});
        }

        private void DoDeleCommand()
        {
            Confirm($"Действительно удалить продукт \"{_prod.Name}\"?", DeleteProduct);
        }

        private async Task DeleteProduct()
        {
            IProductManager manager = Mvx.Resolve<IProductManager>();
            await manager.DeleteProduct(_prod.Id);
            Publish(new ProductListChangedMessage(this));
        }



    }
}
