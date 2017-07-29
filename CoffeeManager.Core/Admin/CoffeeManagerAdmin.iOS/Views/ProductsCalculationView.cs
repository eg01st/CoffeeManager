using System;
using CoffeeManagerAdmin.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
   public partial class ProductsCalculationView : SearchViewController<ProductsCalculationView, ProductsCalculationViewModel, ItemViewModel>
    {
        protected override SimpleTableSource TableSource => new SimpleTableSource(TableView, ItemViewCell.Key, ItemViewCell.Nib);

        protected override UIView TableViewContainer => ContainerView;
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

