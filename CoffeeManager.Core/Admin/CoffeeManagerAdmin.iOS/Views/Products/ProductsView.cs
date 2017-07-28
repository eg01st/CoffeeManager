
using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using System.Collections.Generic;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ProductsView : MvxViewController
    {
        public ProductsView() : base("ProductsView", null)
        {
        }

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

            var source = new SimpleTableSource(ProductsTableView, ProductItemCell.Key, ProductItemCell.Nib);
            ProductsTableView.Source = source;

            var set = this.CreateBindingSet<ProductsView, ProductsViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();

        }
    }
}

