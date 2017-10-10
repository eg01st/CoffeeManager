using System;

using Foundation;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class CashoutTableHeader : UITableViewHeaderFooterView
    {
        public static readonly NSString Key = new NSString("CashoutTableHeader");
        public static readonly UINib Nib;

        static CashoutTableHeader()
        {
            Nib = UINib.FromName("CashoutTableHeader", NSBundle.MainBundle);
        }

        protected CashoutTableHeader(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
