using System;

using UIKit;
using MvvmCross.iOS.Views;
using CoffeeManagerAdmin.Core.ViewModels.Statistic;
using MvvmCross.Binding.BindingContext;
using System.Collections.Generic;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SalesStatisticView : MvxViewController
    {
        public SalesStatisticView() : base("SalesStatisticView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var btn = new UIBarButtonItem();
            btn.Title = "Графики";

            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked ShowChartCommand"},

            });

            Title = "Продажи";
            var tableSource = new SimpleTableSource(TableView,SaleStatisticViewCell.Key, SaleStatisticViewCell.Nib);
            TableView.Source = tableSource;

            var set = this.CreateBindingSet<SalesStatisticView, SalesStatisticViewModel>();
            set.Bind(tableSource).For(i => i.ItemsSource).To(vm => vm.Items);
            set.Apply();
        }

    }
}

