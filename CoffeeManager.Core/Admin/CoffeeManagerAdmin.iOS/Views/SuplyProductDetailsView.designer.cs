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
    [Register ("SuplyProductDetailsView")]
    partial class SuplyProductDetailsView
    {
        [Outlet]
        UIKit.UIButton DeleteButton { get; set; }


        [Outlet]
        UIKit.UITextField ItemCountText { get; set; }


        [Outlet]
        UIKit.UITextField NameText { get; set; }


        [Outlet]
        UIKit.UILabel SalePriceLabel { get; set; }


        [Outlet]
        UIKit.UIButton SaveButton { get; set; }


        [Outlet]
        UIKit.UITextField SuplyPriceText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DeleteButton != null) {
                DeleteButton.Dispose ();
                DeleteButton = null;
            }

            if (ItemCountText != null) {
                ItemCountText.Dispose ();
                ItemCountText = null;
            }

            if (NameText != null) {
                NameText.Dispose ();
                NameText = null;
            }

            if (SalePriceLabel != null) {
                SalePriceLabel.Dispose ();
                SalePriceLabel = null;
            }

            if (SaveButton != null) {
                SaveButton.Dispose ();
                SaveButton = null;
            }

            if (SuplyPriceText != null) {
                SuplyPriceText.Dispose ();
                SuplyPriceText = null;
            }
        }
    }
}