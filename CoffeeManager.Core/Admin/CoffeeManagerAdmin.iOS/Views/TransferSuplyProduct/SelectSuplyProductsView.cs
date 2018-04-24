using System;

using UIKit;
using CoffeManager.Common;
using CoffeeManagerAdmin.Core;
using MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.TransferSuplyProduct;
using CoffeeManagerAdmin.iOS.TableSources;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SelectSuplyProductsView : SearchViewController<SelectSuplyProductsView, SelectSuplyProductsViewModel, ListItemViewModelBase>
    {
        protected override SimpleTableSource TableSource => new SuplyProductTableSource(TableView, TransferProductItemCell.Key, TransferProductItemCell.Nib, SuplyProductHeaderCell.Key, SuplyProductHeaderCell.Nib);

        protected override UIView TableViewContainer => ContainerView;

        public SelectSuplyProductsView() : base("SelectSuplyProductsView", null)
        {
        }

        protected override MvxFluentBindingDescriptionSet<SelectSuplyProductsView, SelectSuplyProductsViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<SelectSuplyProductsView, SelectSuplyProductsViewModel>();
        }

        protected override void DoViewDidLoad()
        {
            Title = "Выбор продуктов";

            var doneButton = new UIBarButtonItem()
            {
                Title = "Готово"
            };

            this.AddBindings(new Dictionary<object, string>
            {
                {doneButton, "Clicked TransferSuplyProductsCommand"},
  
            });

            NavigationItem.SetRightBarButtonItem(doneButton, true);
        }
    }
}

