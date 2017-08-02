﻿using System;
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
        private ProductManager manager = new ProductManager();
        private MvxSubscriptionToken _productListChangedToken;

        private ICommand _addProductCommand;

        public ProductsViewModel()
        {
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
            return items.Select(s => new ProductItemViewModel(s)).ToList();
        }

        public ICommand AddProductCommand => _addProductCommand;
      
    }
}