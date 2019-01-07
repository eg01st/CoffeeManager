// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Categories
{
    [Register ("CategoryTableViewCell")]
    partial class CategoryTableViewCell
    {
        [Outlet]
        UIKit.UILabel CategoryLabel { get; set; }


        [Outlet]
        UIKit.UISwitch IsActiveSwitch { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CategoryLabel != null) {
                CategoryLabel.Dispose ();
                CategoryLabel = null;
            }

            if (IsActiveSwitch != null) {
                IsActiveSwitch.Dispose ();
                IsActiveSwitch = null;
            }
        }
    }
}