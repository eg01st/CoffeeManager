﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.ViewModels.Categories;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.Home
{
    public class ProductsViewModel : BaseSearchViewModel<ListItemViewModelBase>
    {
        private readonly MvxSubscriptionToken productListChangedToken;

        
        private readonly IProductManager manager;
        private readonly MvxSubscriptionToken coffeeRoomChangedToken;

        public ICommand AddProductCommand { get; }
        public ICommand ShowCategoriesCommand { get; }

        
        public ProductsViewModel(IProductManager manager)
        {
            this.manager = manager;
            AddProductCommand = new MvxCommand(DoAddProduct);
            ShowCategoriesCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<CategoriesViewModel>());
            productListChangedToken = MvxMessenger.Subscribe<ProductListChangedMessage>( async(obj) => await Initialize());
            coffeeRoomChangedToken = MvxMessenger.Subscribe<CoffeeRoomChangedMessage>(async (obj) =>await Initialize());
        }


        private void DoAddProduct()
        {
            ShowViewModel<ProductDetailsViewModel>();
        }

        public override async Task<List<ListItemViewModelBase>> LoadData()
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


        protected override void DoUnsubscribe()
        {
            MvxMessenger.Unsubscribe<ProductListChangedMessage>(productListChangedToken);
            MvxMessenger.Unsubscribe<CoffeeRoomChangedMessage>(coffeeRoomChangedToken);
        }
      
    }
}
