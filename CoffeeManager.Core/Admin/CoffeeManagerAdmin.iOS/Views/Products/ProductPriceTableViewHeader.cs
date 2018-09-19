using System;

using Foundation;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ProductPriceTableViewHeader : UITableViewHeaderFooterView
    {
        public static readonly NSString Key = new NSString("ProductPriceTableViewHeader");
        public static readonly UINib Nib;

        static ProductPriceTableViewHeader()
        {
            Nib = UINib.FromName("ProductPriceTableViewHeader", NSBundle.MainBundle);
        }

        protected ProductPriceTableViewHeader(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
