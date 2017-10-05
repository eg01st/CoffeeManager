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
    [Register ("OrderItemViewCell")]
    partial class OrderItemViewCell
    {
        [Outlet]
        UIKit.UIImageView DoneImage { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint DoneImageNormarHeight { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint DoneImageZeroHeight { get; set; }


        [Outlet]
        UIKit.UILabel NameText { get; set; }


        [Outlet]
        UIKit.UILabel PriceLabel { get; set; }


        [Outlet]
        UIKit.UITextField QuantityText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DoneImage != null) {
                DoneImage.Dispose ();
                DoneImage = null;
            }

            if (DoneImageNormarHeight != null) {
                DoneImageNormarHeight.Dispose ();
                DoneImageNormarHeight = null;
            }

            if (DoneImageZeroHeight != null) {
                DoneImageZeroHeight.Dispose ();
                DoneImageZeroHeight = null;
            }

            if (NameText != null) {
                NameText.Dispose ();
                NameText = null;
            }

            if (PriceLabel != null) {
                PriceLabel.Dispose ();
                PriceLabel = null;
            }

            if (QuantityText != null) {
                QuantityText.Dispose ();
                QuantityText = null;
            }
        }
    }
}