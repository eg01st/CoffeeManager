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
    [Register ("CreateInventoryCell")]
    partial class CreateInventoryCell
    {
        [Outlet]
        UIKit.UILabel CurrentCountLabel { get; set; }


        [Outlet]
        UIKit.UIImageView IsSelectedImage { get; set; }


        [Outlet]
        UIKit.UILabel NameLabel { get; set; }


        [Outlet]
        UIKit.UILabel PreviosCountLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CurrentCountLabel != null) {
                CurrentCountLabel.Dispose ();
                CurrentCountLabel = null;
            }

            if (IsSelectedImage != null) {
                IsSelectedImage.Dispose ();
                IsSelectedImage = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }

            if (PreviosCountLabel != null) {
                PreviosCountLabel.Dispose ();
                PreviosCountLabel = null;
            }
        }
    }
}