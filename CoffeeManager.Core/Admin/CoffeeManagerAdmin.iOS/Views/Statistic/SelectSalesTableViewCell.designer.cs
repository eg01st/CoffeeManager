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
    [Register ("SelectSalesTableViewCell")]
    partial class SelectSalesTableViewCell
    {
        [Outlet]
        UIKit.UISwitch IsSelectedSwitch { get; set; }


        [Outlet]
        UIKit.UILabel SaleNameLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (IsSelectedSwitch != null) {
                IsSelectedSwitch.Dispose ();
                IsSelectedSwitch = null;
            }

            if (SaleNameLabel != null) {
                SaleNameLabel.Dispose ();
                SaleNameLabel = null;
            }
        }
    }
}