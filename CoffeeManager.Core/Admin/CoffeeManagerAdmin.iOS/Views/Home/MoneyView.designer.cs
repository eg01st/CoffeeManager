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
        UIKit.UIButton CreditCardButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CurrentShiftMoneyLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel EntireMoneyLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ExpensesButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ProductsButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton RefreshMoneyButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ShiftsButton { get; set; }

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

            if (CreditCardButton != null) {
                CreditCardButton.Dispose ();
                CreditCardButton = null;
            }

            if (CurrentShiftMoneyLabel != null) {
                CurrentShiftMoneyLabel.Dispose ();
                CurrentShiftMoneyLabel = null;
            }

            if (EntireMoneyLabel != null) {
                EntireMoneyLabel.Dispose ();
                EntireMoneyLabel = null;
            }

            if (ExpensesButton != null) {
                ExpensesButton.Dispose ();
                ExpensesButton = null;
            }

            if (ProductsButton != null) {
                ProductsButton.Dispose ();
                ProductsButton = null;
            }

            if (RefreshMoneyButton != null) {
                RefreshMoneyButton.Dispose ();
                RefreshMoneyButton = null;
            }

            if (ShiftsButton != null) {
                ShiftsButton.Dispose ();
                ShiftsButton = null;
            }
        }
    }
}