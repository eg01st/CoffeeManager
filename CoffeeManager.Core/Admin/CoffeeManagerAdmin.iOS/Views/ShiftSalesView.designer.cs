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
    [Register ("ShiftSalesView")]
    partial class ShiftSalesView
    {
        [Outlet]
        UIKit.UIButton ByProductButton { get; set; }


        [Outlet]
        UIKit.UIButton ByTimeButton { get; set; }


        [Outlet]
        UIKit.UITableView SalesTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ByProductButton != null) {
                ByProductButton.Dispose ();
                ByProductButton = null;
            }

            if (ByTimeButton != null) {
                ByTimeButton.Dispose ();
                ByTimeButton = null;
            }

            if (SalesTableView != null) {
                SalesTableView.Dispose ();
                SalesTableView = null;
            }
        }
    }
}