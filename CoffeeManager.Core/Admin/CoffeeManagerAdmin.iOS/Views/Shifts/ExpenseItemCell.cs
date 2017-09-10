using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ExpenseItemCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("ExpenseItemCell");
        public static readonly UINib Nib;

        static ExpenseItemCell()
        {
            Nib = UINib.FromName("ExpenseItemCell", NSBundle.MainBundle);
        }

        protected ExpenseItemCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ExpenseItemCell, ExpenseItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(AmountLabel).To(vm => vm.Amount);
                set.Bind(ItemCountLabel).To(vm => vm.ItemCount);
                set.Apply();
            });
        }

    }
}
