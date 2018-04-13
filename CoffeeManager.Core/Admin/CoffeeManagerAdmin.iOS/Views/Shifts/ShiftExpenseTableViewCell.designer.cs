// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//

using Foundation;

namespace CoffeeManagerAdmin.iOS.Views.Shifts
{
    [Register ("ShiftExpenseTableViewCell")]
    partial class ShiftExpenseTableViewCell
    {
        [Outlet]
        UIKit.UILabel ExpenseQuantitnyName { get; set; }


        [Outlet]
        UIKit.UILabel NameLabel { get; set; }


        [Outlet]
        UIKit.UILabel PriceLabel { get; set; }


        [Outlet]
        UIKit.UILabel QuantityLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ExpenseQuantitnyName != null) {
                ExpenseQuantitnyName.Dispose ();
                ExpenseQuantitnyName = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }

            if (PriceLabel != null) {
                PriceLabel.Dispose ();
                PriceLabel = null;
            }

            if (QuantityLabel != null) {
                QuantityLabel.Dispose ();
                QuantityLabel = null;
            }
        }
    }
}