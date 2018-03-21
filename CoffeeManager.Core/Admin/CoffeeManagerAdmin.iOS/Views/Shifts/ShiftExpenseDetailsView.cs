using System;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ShiftExpenseDetailsView : ViewControllerBase<ShiftExpenseDetailsViewModel>
    {
        public ShiftExpenseDetailsView() : base("ShiftExpenseDetailsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Детали расхода";
            var source = new SimpleTableSource(ExpensesTableView, ShiftExpenseTableViewCell.Key, ShiftExpenseTableViewCell.Nib, ShiftExpenseTableHeader.Key, ShiftExpenseTableHeader.Nib);
            ExpensesTableView.Source = source;

            var set = this.CreateBindingSet<ShiftExpenseDetailsView, ShiftExpenseDetailsViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }
    }
}

