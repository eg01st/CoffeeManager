using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ManageExpensesView : MvxViewController
    {
        public ManageExpensesView() : base("ManageExpensesView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Траты";

            var source = new SimpleTableSource(TableView, ManageExpensesTableViewCell.Key, ManageExpensesTableViewCell.Nib);
            TableView.Source = source;

            var set = this.CreateBindingSet<ManageExpensesView, ManageExpensesViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }

    }
}

