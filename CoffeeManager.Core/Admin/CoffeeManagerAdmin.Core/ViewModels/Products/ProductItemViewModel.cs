using System;
using CoffeeManager.Models;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeeManagerAdmin.Core.ViewModels;
using System.Linq;
using CoffeeManagerAdmin.Core.Util;

namespace CoffeeManagerAdmin.Core
{
    public class ProductItemViewModel : ListItemViewModelBase
    {
        ProductManager manager = new ProductManager();

        private Product _prod;
        private ICommand _deleteProductCommand;
        public ICommand DeleteProductCommand => _deleteProductCommand;
        public ICommand ToggleIsActiveCommand {get;set;}

        private bool _isTapped;

        public string Name {get;set;}
        public string Category {get;set;}
        public bool IsActive {get;set;}

        public ProductItemViewModel(Product prod)
        {
            _prod = prod;
            Name = prod.Name;
            IsActive = prod.IsActive;

            var type = TypesLists.ProductTypesList.First(i => i.Id == prod.ProductType);
            Category = type.Name;
            RaiseAllPropertiesChanged();

            _deleteProductCommand = new MvxCommand(DoDeleCommand);
            ToggleIsActiveCommand = new MvxCommand(DoToggleIsActive);
        }

        private async void DoToggleIsActive()
        {
            await manager.ToggleIsActiveProduct(_prod.Id);
        }

        protected override void DoGoToDetails()
        {
            var id = ParameterTransmitter.PutParameter(_prod);
            ShowViewModel<ProductDetailsViewModel>(new {id});
        }

        private void DoDeleCommand()
        {
            if (!_isTapped)
            {
                _isTapped = true;

                UserDialogs.Confirm(new Acr.UserDialogs.ConfirmConfig()
                {
                    Message = $"Действительно удалить продукт \"{_prod.Name}\"?",
                    OnAction = async (bool obj) =>
                    {
                        if (obj)
                        {
                            await manager.DeleteProduct(_prod.Id);
                            Publish(new ProductListChangedMessage(this));
                        }
                        _isTapped = false;
                    },
                });
            }
        }



    }
}
