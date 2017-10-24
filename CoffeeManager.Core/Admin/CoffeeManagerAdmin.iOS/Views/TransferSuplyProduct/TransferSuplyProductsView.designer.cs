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
    [Register ("TransferSuplyProductsView")]
    partial class TransferSuplyProductsView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField CoffeeRoomFromTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField CoffeeRoomToTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton TransferButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CoffeeRoomFromTextField != null) {
                CoffeeRoomFromTextField.Dispose ();
                CoffeeRoomFromTextField = null;
            }

            if (CoffeeRoomToTextField != null) {
                CoffeeRoomToTextField.Dispose ();
                CoffeeRoomToTextField = null;
            }

            if (TransferButton != null) {
                TransferButton.Dispose ();
                TransferButton = null;
            }
        }
    }
}