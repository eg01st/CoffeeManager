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
    [Register ("SelectSuplyProductItemViewCell")]
    partial class SelectSuplyProductItemViewCell
    {
        [Outlet]
        UIKit.UISwitch IsActiveSwitch { get; set; }


        [Outlet]
        UIKit.UILabel NameLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (IsActiveSwitch != null) {
                IsActiveSwitch.Dispose ();
                IsActiveSwitch = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }
        }
    }
}