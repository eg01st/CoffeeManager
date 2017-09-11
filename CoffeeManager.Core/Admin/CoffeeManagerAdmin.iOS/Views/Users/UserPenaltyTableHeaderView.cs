using System;

using Foundation;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class UserPenaltyTableHeaderView : UITableViewHeaderFooterView
    {
        public static readonly NSString Key = new NSString("UserPenaltyTableHeaderView");
        public static readonly UINib Nib;

        static UserPenaltyTableHeaderView()
        {
            Nib = UINib.FromName("UserPenaltyTableHeaderView", NSBundle.MainBundle);
        }

        protected UserPenaltyTableHeaderView(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
