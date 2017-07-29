using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public class ManageExpensesView : SearchViewController<ManageExpensesView, ManageExpensesViewModel, ManageExpenseItemViewModel>
    {
        protected override SimpleTableSource TableSource => new SimpleTableSource(TableView, ManageExpensesTableViewCell.Key, ManageExpensesTableViewCell.Nib);


        protected override MvxFluentBindingDescriptionSet<ManageExpensesView, ManageExpensesViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<ManageExpensesView, ManageExpensesViewModel>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Траты";
        }

    }
}

