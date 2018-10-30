using System;

using UIKit;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using CoffeeManagerAdmin.Core.ViewModels.AutoOrder;
using CoffeeManagerAdmin.iOS.TableSources;
using MvvmCross.Binding.BindingContext;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SelectSuplyProductsForAutoOrderView : SearchViewController<SelectSuplyProductsForAutoOrderView, SelectSuplyProductsForAutoOrderViewModel, SelectSuplyProductItemViewModel>
    {
        public SelectSuplyProductsForAutoOrderView() : base("SelectSuplyProductsForAutoOrderView", null)
        {
        }

        protected override SimpleTableSource TableSource => throw new NotImplementedException();

        protected override UIView TableViewContainer => throw new NotImplementedException();

        protected override MvxFluentBindingDescriptionSet<SelectSuplyProductsForAutoOrderView, SelectSuplyProductsForAutoOrderViewModel> CreateBindingSet()
        {
            throw new NotImplementedException();
        }
    }
}

