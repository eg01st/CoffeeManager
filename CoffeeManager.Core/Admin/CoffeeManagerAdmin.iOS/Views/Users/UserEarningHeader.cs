using System;

using Foundation;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class UserEarningHeader : UITableViewHeaderFooterView
    {
        public static readonly NSString Key = new NSString("UserEarningHeader");
        public static readonly UINib Nib;

        static UserEarningHeader()
        {
            Nib = UINib.FromName("UserEarningHeader", NSBundle.MainBundle);
        }

        protected UserEarningHeader(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
