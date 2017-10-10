using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class AddExpenseExtendedTableCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("AddExpenseExtendedTableCell");
        public static readonly UINib Nib;

        static AddExpenseExtendedTableCell()
        {
            Nib = UINib.FromName("AddExpenseExtendedTableCell", NSBundle.MainBundle);
        }

        protected AddExpenseExtendedTableCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() => 
            {
                var set = this.CreateBindingSet<AddExpenseExtendedTableCell, AddExtendedExpenseItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(AmountTextField).To(vm => vm.Amount);
                set.Bind(QuantityTextField).To(vm => vm.Quantity);
                set.Bind(NumerationLabel).To(vm => vm.ExpenseNumerationName);
                set.Apply();
            });
        }
    }
}
