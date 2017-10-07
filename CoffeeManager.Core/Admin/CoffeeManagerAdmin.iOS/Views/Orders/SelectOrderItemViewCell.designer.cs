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
    [Register ("SelectOrderItemViewCell")]
    partial class SelectOrderItemViewCell
    {
        [Outlet]
        UIKit.UILabel CountLabel { get; set; }

        [Outlet]
        UIKit.UISwitch IsSelected { get; set; }

        [Outlet]
        UIKit.UILabel NameLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CountLabel != null) {
                CountLabel.Dispose ();
                CountLabel = null;
            }

            if (IsSelected != null) {
                IsSelected.Dispose ();
                IsSelected = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }
        }
    }
}