// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.AutoOrder
{
    [Register ("SuplyProductToOrderItemViewCell")]
    partial class SuplyProductToOrderItemViewCell
    {
        [Outlet]
        UIKit.UITextField QuantityAfterTextField { get; set; }


        [Outlet]
        UIKit.UISwitch ShouldUpdateQuantityBeforeOrderSwitch { get; set; }


        [Outlet]
        UIKit.UILabel SuplyProductNameLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (QuantityAfterTextField != null) {
                QuantityAfterTextField.Dispose ();
                QuantityAfterTextField = null;
            }

            if (ShouldUpdateQuantityBeforeOrderSwitch != null) {
                ShouldUpdateQuantityBeforeOrderSwitch.Dispose ();
                ShouldUpdateQuantityBeforeOrderSwitch = null;
            }

            if (SuplyProductNameLabel != null) {
                SuplyProductNameLabel.Dispose ();
                SuplyProductNameLabel = null;
            }
        }
    }
}