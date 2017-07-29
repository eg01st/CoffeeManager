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
    [Register ("ItemViewCell")]
    partial class ItemViewCell
    {
        [Outlet]
        UIKit.UILabel Name { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Name != null) {
                Name.Dispose ();
                Name = null;
            }
        }
    }
}