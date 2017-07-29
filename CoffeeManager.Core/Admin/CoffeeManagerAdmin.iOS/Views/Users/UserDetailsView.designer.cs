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
    [Register ("UserDetailsView")]
    partial class UserDetailsView
    {
        [Outlet]
        UIKit.UILabel CurrentSalaryLabel { get; set; }


        [Outlet]
        UIKit.UITextField DayPercentageTextField { get; set; }


        [Outlet]
        UIKit.UILabel EntireSalaryLabel { get; set; }


        [Outlet]
        UIKit.UITextField ExpenseTypeTextField { get; set; }


        [Outlet]
        UIKit.UITextField NameTextField { get; set; }


        [Outlet]
        UIKit.UITextField NightPercentageTextField { get; set; }


        [Outlet]
        UIKit.UIButton PaySalaryButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CurrentSalaryLabel != null) {
                CurrentSalaryLabel.Dispose ();
                CurrentSalaryLabel = null;
            }

            if (DayPercentageTextField != null) {
                DayPercentageTextField.Dispose ();
                DayPercentageTextField = null;
            }

            if (EntireSalaryLabel != null) {
                EntireSalaryLabel.Dispose ();
                EntireSalaryLabel = null;
            }

            if (ExpenseTypeTextField != null) {
                ExpenseTypeTextField.Dispose ();
                ExpenseTypeTextField = null;
            }

            if (NameTextField != null) {
                NameTextField.Dispose ();
                NameTextField = null;
            }

            if (NightPercentageTextField != null) {
                NightPercentageTextField.Dispose ();
                NightPercentageTextField = null;
            }

            if (PaySalaryButton != null) {
                PaySalaryButton.Dispose ();
                PaySalaryButton = null;
            }
        }
    }
}