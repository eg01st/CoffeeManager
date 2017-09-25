using System;
using System.Collections.Generic;
using CoffeeManager.Models;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManagerAdmin.Core.ViewModels;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core
{
    public class ProductsViewModel : BaseSearchViewModel<ProductItemViewModel>
    {
        private MvxSubscriptionToken _productListChangedToken;

        private ICommand _addProductCommand;
        readonly IProductManager manager;

        public ProductsViewModel(IProductManager manager)
        {
            this.manager = manager;
            _addProductCommand = new MvxCommand(DoAddProduct);
            _productListChangedToken = Subscribe<ProductListChangedMessage>((obj) =>
            {
                Init();
            });
        }


        private void DoAddProduct()
        {
            ShowViewModel<ProductDetailsViewModel>();
        }

        public async override Task<List<ProductItemViewModel>> LoadData()
        {
            var items = await manager.GetProducts();
            return items.Select(s => new ProductItemViewModel(manager, s)).ToList();
        }

        public ICommand AddProductCommand => _addProductCommand;

        protected override void DoUnsubscribe()
        {
            Unsubscribe<ProductListChangedMessage>(_productListChangedToken);
        }
      
    }
}
