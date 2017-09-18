using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ShiftExpenseTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("ShiftExpenseTableViewCell");
        public static readonly UINib Nib;

        static ShiftExpenseTableViewCell()
        {
            Nib = UINib.FromName("ShiftExpenseTableViewCell", NSBundle.MainBundle);
        }

        protected ShiftExpenseTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() => 
            {
                var set = this.CreateBindingSet<ShiftExpenseTableViewCell, SupliedProduct>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(QuantityLabel).To(vm=> vm.Quatity);
                set.Bind(ExpenseQuantitnyName).To(vm => vm.ExpenseNumerationName);
                set.Bind(PriceLabel).To(vm => vm.Price).WithConversion(new DecimalToStringConverter());
                set.Apply();
            });
        }
    }
}
