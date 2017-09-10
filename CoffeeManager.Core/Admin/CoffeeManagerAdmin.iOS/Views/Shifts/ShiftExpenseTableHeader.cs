using System;

using Foundation;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ShiftExpenseTableHeader : UITableViewHeaderFooterView
    {
        public static readonly NSString Key = new NSString("ShiftExpenseTableHeader");
        public static readonly UINib Nib;

        static ShiftExpenseTableHeader()
        {
            Nib = UINib.FromName("ShiftExpenseTableHeader", NSBundle.MainBundle);
        }

        protected ShiftExpenseTableHeader(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
