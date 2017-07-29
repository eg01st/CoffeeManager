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
    [Register ("ProductDetailsView")]
    partial class ProductDetailsView
    {
        [Outlet]
        UIKit.UIButton AddProductButton { get; set; }


        [Outlet]
        UIKit.UITextField CupTypeCategoryText { get; set; }


        [Outlet]
        UIKit.UITextField NameText { get; set; }


        [Outlet]
        UIKit.UITextField PolicePriceText { get; set; }


        [Outlet]
        UIKit.UITextField PriceText { get; set; }


        [Outlet]
        UIKit.UITextField ProductTypeText { get; set; }


        [Outlet]
        UIKit.UITextField SuplyBindLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AddProductButton != null) {
                AddProductButton.Dispose ();
                AddProductButton = null;
            }

            if (CupTypeCategoryText != null) {
                CupTypeCategoryText.Dispose ();
                CupTypeCategoryText = null;
            }

            if (NameText != null) {
                NameText.Dispose ();
                NameText = null;
            }

            if (PolicePriceText != null) {
                PolicePriceText.Dispose ();
                PolicePriceText = null;
            }

            if (PriceText != null) {
                PriceText.Dispose ();
                PriceText = null;
            }

            if (ProductTypeText != null) {
                ProductTypeText.Dispose ();
                ProductTypeText = null;
            }
        }
    }
}