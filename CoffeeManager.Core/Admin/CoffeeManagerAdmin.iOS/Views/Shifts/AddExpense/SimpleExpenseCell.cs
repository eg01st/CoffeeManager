using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SimpleExpenseCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("SimpleExpenseCell");
        public static readonly UINib Nib;

        static SimpleExpenseCell()
        {
            Nib = UINib.FromName("SimpleExpenseCell", NSBundle.MainBundle);
        }

        protected SimpleExpenseCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() => 
            {
                var set = this.CreateBindingSet<SimpleExpenseCell, AddExpenseItemViewModel>();
                set.Bind(SimpeExpenseName).To(vm => vm.Name);
                set.Apply();
            });
        }
    }
}
