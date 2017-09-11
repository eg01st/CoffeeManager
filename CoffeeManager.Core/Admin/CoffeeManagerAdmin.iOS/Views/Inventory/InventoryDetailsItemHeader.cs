using System;

using Foundation;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class InventoryDetailsItemHeader : UITableViewHeaderFooterView
    {
        public static readonly NSString Key = new NSString("InventoryDetailsItemHeader");
        public static readonly UINib Nib;

        static InventoryDetailsItemHeader()
        {
            Nib = UINib.FromName("InventoryDetailsItemHeader", NSBundle.MainBundle);
        }

        protected InventoryDetailsItemHeader(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
