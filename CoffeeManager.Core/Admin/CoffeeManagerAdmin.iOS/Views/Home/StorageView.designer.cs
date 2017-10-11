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
    [Register ("StorageView")]
    partial class StorageView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton InventoryButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton OrdersButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SuplyProductsButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton UtilizeButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (InventoryButton != null) {
                InventoryButton.Dispose ();
                InventoryButton = null;
            }

            if (OrdersButton != null) {
                OrdersButton.Dispose ();
                OrdersButton = null;
            }

            if (SuplyProductsButton != null) {
                SuplyProductsButton.Dispose ();
                SuplyProductsButton = null;
            }

            if (UtilizeButton != null) {
                UtilizeButton.Dispose ();
                UtilizeButton = null;
            }
        }
    }
}