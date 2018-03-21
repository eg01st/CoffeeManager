
using UIKit;
using CoffeeManagerAdmin.Core;
using MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using CoffeeManagerAdmin.iOS.Views.Abstract;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SelectSalesView : SearchViewController<SelectSalesView, SelectSalesViewModel, SelectSaleItemViewModel>
    {
        public SelectSalesView() : base("SelectSalesView", null)
        {
        }

        protected override SimpleTableSource TableSource => new SimpleTableSource(TableView, SelectSalesTableViewCell.Key, SelectSalesTableViewCell.Nib);

        protected override UIView TableViewContainer => ContainerView;

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
        }

        protected override MvxFluentBindingDescriptionSet<SelectSalesView, SelectSalesViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<SelectSalesView, SelectSalesViewModel>();
        }
    }
}

