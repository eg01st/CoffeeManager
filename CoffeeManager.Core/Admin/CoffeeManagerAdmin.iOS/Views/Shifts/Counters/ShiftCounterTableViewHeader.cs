using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Shifts.Counters
{
    public partial class ShiftCounterTableViewHeader : MvxTableViewHeaderFooterView
    {
        public static readonly NSString Key = new NSString("ShiftCounterTableViewHeader");
        public static readonly UINib Nib;

        static ShiftCounterTableViewHeader()
        {
            Nib = UINib.FromName("ShiftCounterTableViewHeader", NSBundle.MainBundle);
        }

        protected ShiftCounterTableViewHeader(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
