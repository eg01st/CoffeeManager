﻿using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels.Orders;
using System.Collections.Generic;
using System.Drawing;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SelectOrderItemsView : ViewControllerBase<SelectOrderItemsViewModel>
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

            NavigationItem.SetLeftBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked DoneCommand"}

            });

            var btn1 = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_add_circle_outline")
            };

            NavigationItem.SetRightBarButtonItem(btn1, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn1, "Clicked AddNewSuplyProductCommand"},

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
            set.Bind(AddProdButton).To(vm => vm.AddNewSuplyProductCommand);
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

