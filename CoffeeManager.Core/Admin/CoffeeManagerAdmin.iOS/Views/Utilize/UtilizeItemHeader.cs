using System;

using Foundation;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class UtilizeItemHeader : UITableViewHeaderFooterView
    {
        public static readonly NSString Key = new NSString("UtilizeItemHeader");
        public static readonly UINib Nib;

        static UtilizeItemHeader()
        {
            Nib = UINib.FromName("UtilizeItemHeader", NSBundle.MainBundle);
        }

        protected UtilizeItemHeader(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
