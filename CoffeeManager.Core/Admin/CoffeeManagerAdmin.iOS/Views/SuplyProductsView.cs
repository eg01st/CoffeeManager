﻿using CoffeeManagerAdmin.Core.ViewModels;
using UIKit;
using MvvmCross.Binding.BindingContext;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SuplyProductsView : SearchViewController<SuplyProductsView, SuplyProductsViewModel, SuplyProductItemViewModel>
    {
        protected override SimpleTableSource TableSource => new SimpleTableSource(TableView, SuplyProductCell.Key, SuplyProductCell.Nib);

        protected override UIView TableViewContainer => ContainerView;

        public SuplyProductsView() : base("SuplyProductsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Баланс товара";
        }


        protected override MvxFluentBindingDescriptionSet<SuplyProductsView, SuplyProductsViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<SuplyProductsView, SuplyProductsViewModel>();
        }
    }
}
