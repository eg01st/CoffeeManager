// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS
{
    [Register ("AddExpenseExtendedTableCell")]
    partial class AddExpenseExtendedTableCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField AmountTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NumerationLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField QuantityTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AmountTextField != null) {
                AmountTextField.Dispose ();
                AmountTextField = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }

            if (NumerationLabel != null) {
                NumerationLabel.Dispose ();
                NumerationLabel = null;
            }

            if (QuantityTextField != null) {
                QuantityTextField.Dispose ();
                QuantityTextField = null;
            }
        }
    }
}