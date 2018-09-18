using System;
using Foundation;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Motivation
{
    public partial class MotivationTableViewHeader : UITableViewHeaderFooterView
    {
        public static readonly NSString Key = new NSString("MotivationTableViewHeader");
        public static readonly UINib Nib;

        static MotivationTableViewHeader()
        {
            Nib = UINib.FromName("MotivationTableViewHeader", NSBundle.MainBundle);
        }

        protected MotivationTableViewHeader(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
