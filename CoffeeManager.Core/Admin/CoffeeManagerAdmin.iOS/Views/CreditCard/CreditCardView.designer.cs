// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.CreditCard
{
    [Register ("CreditCardView")]
    partial class CreditCardView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField CashoutAmountTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CashoutButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView CashoutTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField CurrentAmountTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SaveCurrentAmountButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CashoutAmountTextField != null) {
                CashoutAmountTextField.Dispose ();
                CashoutAmountTextField = null;
            }

            if (CashoutButton != null) {
                CashoutButton.Dispose ();
                CashoutButton = null;
            }

            if (CashoutTableView != null) {
                CashoutTableView.Dispose ();
                CashoutTableView = null;
            }

            if (CurrentAmountTextField != null) {
                CurrentAmountTextField.Dispose ();
                CurrentAmountTextField = null;
            }

            if (SaveCurrentAmountButton != null) {
                SaveCurrentAmountButton.Dispose ();
                SaveCurrentAmountButton = null;
            }
        }
    }
}