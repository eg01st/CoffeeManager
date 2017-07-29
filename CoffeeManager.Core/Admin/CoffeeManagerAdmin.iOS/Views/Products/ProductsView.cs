
using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using System.Collections.Generic;
using System;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ProductsView : SearchViewController<ProductsView, ProductsViewModel, ProductItemViewModel>
    {
        protected override SimpleTableSource TableSource => new SimpleTableSource(TableView, ProductItemCell.Key, ProductItemCell.Nib);
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           
            Title = "Продукты";

            var btn = new UIBarButtonItem();
            btn.Title = "Добавить";

            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked AddProductCommand"},
            });
        }

        protected override MvxFluentBindingDescriptionSet<ProductsView, ProductsViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<ProductsView, ProductsViewModel>();
        }
    }
}

