using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ProductsCalculationView : SearchViewController<ProductsCalculationView, ProductsCalculationViewModel, ItemViewModel>
    {
        protected override SimpleTableSource TableSource => new SimpleTableSource(TableView, ItemViewCell.Key, ItemViewCell.Nib);

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Калькуляция продуктов";
        }

        protected override MvxFluentBindingDescriptionSet<ProductsCalculationView, ProductsCalculationViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<ProductsCalculationView, ProductsCalculationViewModel>();
        }
    }
}

