// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Home
{
    [Register ("MoneyView")]
    partial class MoneyView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField CoffeeRoomTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CreditCardAmountLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CurrentShiftMoneyLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel EntireMoneyLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton RefreshMoneyButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView ShiftsTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CoffeeRoomTextField != null) {
                CoffeeRoomTextField.Dispose ();
                CoffeeRoomTextField = null;
            }

            if (CreditCardAmountLabel != null) {
                CreditCardAmountLabel.Dispose ();
                CreditCardAmountLabel = null;
            }

            if (CurrentShiftMoneyLabel != null) {
                CurrentShiftMoneyLabel.Dispose ();
                CurrentShiftMoneyLabel = null;
            }

            if (EntireMoneyLabel != null) {
                EntireMoneyLabel.Dispose ();
                EntireMoneyLabel = null;
            }

            if (RefreshMoneyButton != null) {
                RefreshMoneyButton.Dispose ();
                RefreshMoneyButton = null;
            }

            if (ShiftsTableView != null) {
                ShiftsTableView.Dispose ();
                ShiftsTableView = null;
            }
        }
    }
}