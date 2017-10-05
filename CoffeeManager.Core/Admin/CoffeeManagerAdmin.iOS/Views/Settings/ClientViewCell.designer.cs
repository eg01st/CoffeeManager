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
    [Register ("ClientViewCell")]
    partial class ClientViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ClientNameLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ClientNameLabel != null) {
                ClientNameLabel.Dispose ();
                ClientNameLabel = null;
            }
        }
    }
}