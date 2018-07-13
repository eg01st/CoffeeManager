using System;

using Foundation;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class PaymentStrategyTableViewHeader : UITableViewHeaderFooterView
    {
        public static readonly NSString Key = new NSString("PaymentStrategyTableViewHeader");
        public static readonly UINib Nib;

        static PaymentStrategyTableViewHeader()
        {
            Nib = UINib.FromName("PaymentStrategyTableViewHeader", NSBundle.MainBundle);
        }

        protected PaymentStrategyTableViewHeader(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
