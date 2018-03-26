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
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class ProductsViewModel : BaseSearchViewModel<ListItemViewModelBase>
    {
        private MvxSubscriptionToken _productListChangedToken;

        private ICommand _addProductCommand;
        readonly IProductManager manager;
        private readonly MvxSubscriptionToken _coffeeRoomChangedToken;

        public ProductsViewModel(IProductManager manager)
        {
            this.manager = manager;
            _addProductCommand = new MvxCommand(DoAddProduct);
            _productListChangedToken = MvxMessenger.Subscribe<ProductListChangedMessage>( async(obj) =>
            {
                await Initialize();
            });
            _coffeeRoomChangedToken = MvxMessenger.Subscribe<CoffeeRoomChangedMessage>(async (obj) =>
            {
                await Initialize();
            });

        }


        private void DoAddProduct()
        {
            ShowViewModel<ProductDetailsViewModel>();
        }

        public async override Task<List<ListItemViewModelBase>> LoadData()
        {
            var items = await manager.GetProducts();
            var result = new List<ListItemViewModelBase>();


            var vms = items.Select(s => new ProductItemViewModel(s)).GroupBy(g => g.Category).OrderBy(o => o.Key);
            foreach (var item in vms)
            {
                result.Add(new ExpenseTypeHeaderViewModel(item.Key));
                result.AddRange(item);
            }
            return result;
        }

        public ICommand AddProductCommand => _addProductCommand;

        protected override void DoUnsubscribe()
        {
            MvxMessenger.Unsubscribe<ProductListChangedMessage>(_productListChangedToken);
            MvxMessenger.Unsubscribe<CoffeeRoomChangedMessage>(_coffeeRoomChangedToken);
        }
      
    }
}
