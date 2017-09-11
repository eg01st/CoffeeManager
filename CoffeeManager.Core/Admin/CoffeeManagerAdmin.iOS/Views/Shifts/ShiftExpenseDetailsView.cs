using System;
using CoffeeManagerAdmin.Core;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ShiftExpenseDetailsView : ViewControllerBase
    {
        public ShiftExpenseDetailsView() : base("ShiftExpenseDetailsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Детали расхода";
            var source = new SimpleTableSourceWithHeader(ExpensesTableView, ShiftExpenseTableViewCell.Key, ShiftExpenseTableViewCell.Nib, ShiftExpenseTableHeader.Key);
            ExpensesTableView.Source = source;
            ExpensesTableView.RegisterNibForHeaderFooterViewReuse(ShiftExpenseTableHeader.Nib, ShiftExpenseTableHeader.Key);

            var set = this.CreateBindingSet<ShiftExpenseDetailsView, ShiftExpenseDetailsViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }
    }
}

