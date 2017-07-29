// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS
{
    [Register ("CalculationItemViewCell")]
    partial class CalculationItemViewCell
    {
        [Outlet]
        UIKit.UIButton DeleteButton { get; set; }


        [Outlet]
        UIKit.UILabel NameLabel { get; set; }


        [Outlet]
        UIKit.UILabel QuantityLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DeleteButton != null) {
                DeleteButton.Dispose ();
                DeleteButton = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }

            if (QuantityLabel != null) {
                QuantityLabel.Dispose ();
                QuantityLabel = null;
            }
        }
    }
}