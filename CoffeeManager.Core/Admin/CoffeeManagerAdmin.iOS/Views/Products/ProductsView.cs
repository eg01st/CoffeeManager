using System;
using System.Collections.Generic;
using CoffeeManagerAdmin.Core;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ProductsView : SearchViewController<ProductsView, ProductsViewModel, ProductItemViewModel>
    {
        protected override SimpleTableSource TableSource => new SimpleTableSource(TableView, ProductItemCell.Key, ProductItemCell.Nib);

        protected override UIView TableViewContainer => this.ContainerView;

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

