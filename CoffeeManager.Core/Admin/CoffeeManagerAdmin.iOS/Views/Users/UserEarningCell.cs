using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.iOS.Converters;

namespace CoffeeManagerAdmin.iOS
{
    public partial class UserEarningCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("UserEarningCell");
        public static readonly UINib Nib;

        static UserEarningCell()
        {
            Nib = UINib.FromName("UserEarningCell", NSBundle.MainBundle);
        }

        protected UserEarningCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<UserEarningCell, UserEarningItemViewModel>();
                set.Bind(DateLabel).To(vm => vm.Date);
                set.Bind(AmountLabel).To(vm => vm.Amount).WithConversion(new DecimalToStringConverter());
                set.Bind(ShiftLabel).To(vm => vm.ShiftType);
                set.Apply();
            });
        }
    }
}
