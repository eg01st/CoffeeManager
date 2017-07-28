using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ProductsCalculationView : MvxViewController
    {
        public ProductsCalculationView() : base("ProductsCalculationView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source = new SimpleTableSource(ProductsTableView, ItemViewCell.Key, ItemViewCell.Nib);
            ProductsTableView.Source = source;

            var set = this.CreateBindingSet<ProductsCalculationView, ProductsCalculationViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }

    }
}

