using System;

using UIKit;
using MvvmCross.iOS.Views;
using CoffeeManagerAdmin.Core;
using MvvmCross.Binding.BindingContext;
using System.Collections.Generic;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SelectSalesView : MvxViewController
    {
        public SelectSalesView() : base("SelectSalesView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Выбор продуктов";
            var btn = new UIBarButtonItem();
            btn.Title = "Готово";
            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked ShowChartCommand"},
            });

            var tableSource = new SimpleTableSource(TableView, SelectSalesTableViewCell.Key, SelectSalesTableViewCell.Nib);
            TableView.Source = tableSource;

            var set = this.CreateBindingSet<SelectSalesView, SelectSalesViewModel>();
            set.Bind(tableSource).For(i => i.ItemsSource).To(vm => vm.Items);
            set.Apply();            

        }
    }
}

