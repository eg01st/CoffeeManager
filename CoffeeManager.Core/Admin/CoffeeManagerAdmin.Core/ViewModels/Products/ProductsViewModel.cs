using System;
using System.Collections.Generic;
using CoffeeManager.Models;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManagerAdmin.Core.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class ProductsViewModel : ViewModelBase
    {
        private ProductManager manager = new ProductManager();
        private MvxSubscriptionToken _productListChangedToken;

        private ICommand _addProductCommand;

        private List<ProductItemViewModel> _items = new List<ProductItemViewModel>();

      

        public List<ProductItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }


        public ProductsViewModel()
        {
            _addProductCommand = new MvxCommand(DoAddProduct);
            _productListChangedToken = Subscribe<ProductListChangedMessage>(async (obj) =>
            {
                await LoadProducts();
            });
        }

        public async void Init()
        {
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var items = await manager.GetProducts();
            Items = items.Select(s => new ProductItemViewModel(s)).ToList();
        }

        private void DoAddProduct()
        {
            ShowViewModel<ProductDetailsViewModel>();
        }

        public ICommand AddProductCommand => _addProductCommand;
      
    }
}
