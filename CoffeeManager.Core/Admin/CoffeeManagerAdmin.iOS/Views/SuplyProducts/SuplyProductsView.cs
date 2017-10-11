﻿using CoffeeManagerAdmin.Core.ViewModels;
using UIKit;
using MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SuplyProductsView : SearchViewController<SuplyProductsView, SuplyProductsViewModel, ListItemViewModelBase>
    {
        protected override SimpleTableSource TableSource => new SuplyProductTableSource(TableView, SuplyProductCell.Key, SuplyProductCell.Nib, SuplyProductHeaderCell.Key, SuplyProductHeaderCell.Nib);

        protected override UIView TableViewContainer => ContainerView;

        public SuplyProductsView() : base("SuplyProductsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Баланс продуктов";

            var btn = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_add_circle_outline")
            };


            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked AddNewSuplyProductCommand"},
            });
        }


        protected override MvxFluentBindingDescriptionSet<SuplyProductsView, SuplyProductsViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<SuplyProductsView, SuplyProductsViewModel>();
        }
    }
}

