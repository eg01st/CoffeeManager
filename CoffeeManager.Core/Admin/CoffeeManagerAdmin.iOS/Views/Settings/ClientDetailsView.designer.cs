// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    [Register ("ClientDetailsView")]
    partial class ClientDetailsView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ApiUrlLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ClientNameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DeleteUserButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ApiUrlLabel != null) {
                ApiUrlLabel.Dispose ();
                ApiUrlLabel = null;
            }

            if (ClientNameLabel != null) {
                ClientNameLabel.Dispose ();
                ClientNameLabel = null;
            }

            if (DeleteUserButton != null) {
                DeleteUserButton.Dispose ();
                DeleteUserButton = null;
            }
        }
    }
}