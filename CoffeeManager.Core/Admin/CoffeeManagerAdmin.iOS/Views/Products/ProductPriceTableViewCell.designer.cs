// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Products
{
    [Register ("ProductPriceTableViewCell")]
    partial class ProductPriceTableViewCell
    {
        [Outlet]
        UIKit.UILabel CoffeeRoomNameLabel { get; set; }


        [Outlet]
        UIKit.UITextField DiscountPriceTextField { get; set; }


        [Outlet]
        UIKit.UITextField PriceTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CoffeeRoomNameLabel != null) {
                CoffeeRoomNameLabel.Dispose ();
                CoffeeRoomNameLabel = null;
            }

            if (DiscountPriceTextField != null) {
                DiscountPriceTextField.Dispose ();
                DiscountPriceTextField = null;
            }

            if (PriceTextField != null) {
                PriceTextField.Dispose ();
                PriceTextField = null;
            }
        }
    }
}