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
    [Register ("PaymentStrategyTableViewCell")]
    partial class PaymentStrategyTableViewCell
    {
        [Outlet]
        UIKit.UILabel CoffeeRoomNameLabel { get; set; }


        [Outlet]
        UIKit.UITextField DayPercentTextField { get; set; }


        [Outlet]
        UIKit.UITextField NightPercentTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CoffeeRoomNameLabel != null) {
                CoffeeRoomNameLabel.Dispose ();
                CoffeeRoomNameLabel = null;
            }

            if (DayPercentTextField != null) {
                DayPercentTextField.Dispose ();
                DayPercentTextField = null;
            }

            if (NightPercentTextField != null) {
                NightPercentTextField.Dispose ();
                NightPercentTextField = null;
            }
        }
    }
}