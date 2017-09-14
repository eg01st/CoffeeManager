using System;
using System.Collections.Generic;
using CoffeeManagerAdmin.Core;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class MapExpenseToSuplyProductView  : SearchViewController<MapExpenseToSuplyProductView, MapExpenseToSuplyProductViewModel, SelectMappedSuplyProductItemViewModel>
    {
        protected override SimpleTableSource TableSource => new SimpleTableSource(TableView, MappedSuplyProductsTableViewCell.Key, MappedSuplyProductsTableViewCell.Nib);

        protected override UIView TableViewContainer => this.ContainerView;

        protected override MvxFluentBindingDescriptionSet<MapExpenseToSuplyProductView, MapExpenseToSuplyProductViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<MapExpenseToSuplyProductView, MapExpenseToSuplyProductViewModel>();
        }

        public MapExpenseToSuplyProductView() : base("MapExpenseToSuplyProductView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var btn = new UIBarButtonItem()
            {
                Title = "Создать товар"
            };


            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked AddNewSuplyProductCommand"},

            });
        }

    }
}

