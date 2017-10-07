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
    [Register ("InventoryDetailsItemCell")]
    partial class InventoryDetailsItemCell
    {
        [Outlet]
        UIKit.UILabel AfterLabel { get; set; }


        [Outlet]
        UIKit.UILabel BeforeLabel { get; set; }


        [Outlet]
        UIKit.UILabel DiffLabel { get; set; }


        [Outlet]
        UIKit.UILabel NameLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AfterLabel != null) {
                AfterLabel.Dispose ();
                AfterLabel = null;
            }

            if (BeforeLabel != null) {
                BeforeLabel.Dispose ();
                BeforeLabel = null;
            }

            if (DiffLabel != null) {
                DiffLabel.Dispose ();
                DiffLabel = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }
        }
    }
}