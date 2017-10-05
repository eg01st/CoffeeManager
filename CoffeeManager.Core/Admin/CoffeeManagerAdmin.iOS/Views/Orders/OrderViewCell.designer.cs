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
    [Register ("OrderViewCell")]
    partial class OrderViewCell
    {
        [Outlet]
        UIKit.UILabel DisplayLabel { get; set; }


        [Outlet]
        UIKit.UILabel StatusLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DisplayLabel != null) {
                DisplayLabel.Dispose ();
                DisplayLabel = null;
            }

            if (StatusLabel != null) {
                StatusLabel.Dispose ();
                StatusLabel = null;
            }
        }
    }
}