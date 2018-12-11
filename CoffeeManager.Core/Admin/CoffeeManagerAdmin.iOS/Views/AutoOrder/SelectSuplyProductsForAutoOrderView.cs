using System;

using UIKit;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using CoffeeManagerAdmin.Core.ViewModels.AutoOrder;
using CoffeeManagerAdmin.iOS.TableSources;
using MvvmCross.Binding.BindingContext;
using System.Collections.Generic;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SelectSuplyProductsForAutoOrderView : SearchViewController<SelectSuplyProductsForAutoOrderView, SelectSuplyProductsForAutoOrderViewModel, SelectSuplyProductItemViewModel>
    {
        public SelectSuplyProductsForAutoOrderView() : base("SelectSuplyProductsForAutoOrderView", null)
        {
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();
            Title = "Товары для автозаказа";

            var btn = new UIBarButtonItem()
            {
                Title = "Готово"
            };
            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked DoneCommand"},
            });
        }

        protected override SimpleTableSource TableSource => new SimpleTableSource(TableView, SelectSuplyProductItemViewCell.Key, SelectSuplyProductItemViewCell.Nib);

        protected override UIView TableViewContainer => TableContainerView;

        protected override MvxFluentBindingDescriptionSet<SelectSuplyProductsForAutoOrderView, SelectSuplyProductsForAutoOrderViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<SelectSuplyProductsForAutoOrderView, SelectSuplyProductsForAutoOrderViewModel>();
        }
    }
}

