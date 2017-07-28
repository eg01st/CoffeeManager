using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels.Orders;
using System.Collections.Generic;
using System.Drawing;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SelectOrderItemsView : MvxViewController
    {
        public SelectOrderItemsView() : base("SelectOrderItemsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.SetHidesBackButton(true, false);

            var btn = new UIBarButtonItem()
            {
                Title = "Готово"
            };

            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked DoneCommand"}

            });

            var _searchBar = new UISearchBar(new RectangleF(0, 0, 320, 44))
            {
                AutocorrectionType = UITextAutocorrectionType.No
            };


            var source = new SimpleTableSource(ProductsTableView, SelectOrderItemViewCell.Key, SelectOrderItemViewCell.Nib);
            ProductsTableView.Source = source;
            ProductsTableView.TableHeaderView = _searchBar;

            var set = this.CreateBindingSet<SelectOrderItemsView, SelectOrderItemsViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Bind(NewProductText).To(vm => vm.NewProductName);
            set.Bind(AddProdButton).To(vm => vm.AddNewProductCommand);
            set.Bind(_searchBar).For(v => v.Text).To(vm => vm.SearchString);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }


    }
}

