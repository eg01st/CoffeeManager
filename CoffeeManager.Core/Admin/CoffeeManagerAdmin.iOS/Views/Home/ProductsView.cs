using System;
using System.Collections.Generic;
using CoffeeManagerAdmin.Core;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ProductsView : SearchViewController<ProductsView, ProductsViewModel, ListItemViewModelBase>
    {
        protected override SimpleTableSource TableSource => new SuplyProductTableSource(TableView, ProductItemCell.Key, ProductItemCell.Nib, SuplyProductHeaderCell.Key, SuplyProductHeaderCell.Nib);

        protected override UIView TableViewContainer => this.ContainerView;

        protected override bool UseCustomBackButton => false;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           
            Title = "Товары";

            var btn = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_add_circle_outline")
            };

            NavigationItem.SetRightBarButtonItem(btn, true);
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

