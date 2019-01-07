// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS
{
    [Register ("AddAutoOrderView")]
    partial class AddAutoOrderView
    {
        [Outlet]
        UIKit.UIButton AddSuplyProductsButton { get; set; }


        [Outlet]
        UIKit.UITextField HourTextField { get; set; }


        [Outlet]
        UIKit.UITableView SUplyProductsTableView { get; set; }


        [Outlet]
        UIKit.UITextField WeekDayTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AddSuplyProductsButton != null) {
                AddSuplyProductsButton.Dispose ();
                AddSuplyProductsButton = null;
            }

            if (HourTextField != null) {
                HourTextField.Dispose ();
                HourTextField = null;
            }

            if (SUplyProductsTableView != null) {
                SUplyProductsTableView.Dispose ();
                SUplyProductsTableView = null;
            }

            if (WeekDayTextField != null) {
                WeekDayTextField.Dispose ();
                WeekDayTextField = null;
            }
        }
    }
}