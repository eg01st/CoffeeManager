using System;

using Foundation;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class AddExpenseExtendedTableHeader : UITableViewHeaderFooterView
    {
        public static readonly NSString Key = new NSString("AddExpenseExtendedTableHeader");
        public static readonly UINib Nib;

        static AddExpenseExtendedTableHeader()
        {
            Nib = UINib.FromName("AddExpenseExtendedTableHeader", NSBundle.MainBundle);
        }

        protected AddExpenseExtendedTableHeader(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
