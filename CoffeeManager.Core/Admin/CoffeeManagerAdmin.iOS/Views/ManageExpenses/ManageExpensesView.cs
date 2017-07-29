using System;
using CoffeeManagerAdmin.Core;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ManageExpensesView : SearchViewController<ManageExpensesView, ManageExpensesViewModel, ManageExpenseItemViewModel>
    {
        protected override SimpleTableSource TableSource => new SimpleTableSource(TableView, ManageExpensesTableViewCell.Key, ManageExpensesTableViewCell.Nib);

        protected override UIView TableViewContainer => this.ContainerView;

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

