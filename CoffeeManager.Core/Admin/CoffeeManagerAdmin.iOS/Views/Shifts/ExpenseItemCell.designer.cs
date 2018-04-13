// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//

using Foundation;

namespace CoffeeManagerAdmin.iOS.Views.Shifts
{
    [Register ("ExpenseItemCell")]
    partial class ExpenseItemCell
    {
        [Outlet]
        UIKit.UILabel AmountLabel { get; set; }


        [Outlet]
        UIKit.UILabel ItemCountLabel { get; set; }


        [Outlet]
        UIKit.UILabel NameLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AmountLabel != null) {
                AmountLabel.Dispose ();
                AmountLabel = null;
            }

            if (ItemCountLabel != null) {
                ItemCountLabel.Dispose ();
                ItemCountLabel = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }
        }
    }
}