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
    [Register ("ShiftSalesView")]
    partial class ShiftSalesView
    {
        [Outlet]
        UIKit.UITableView SalesTableView { get; set; }


        [Outlet]
        UIKit.UISegmentedControl SegmentControl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (SalesTableView != null) {
                SalesTableView.Dispose ();
                SalesTableView = null;
            }

            if (SegmentControl != null) {
                SegmentControl.Dispose ();
                SegmentControl = null;
            }
        }
    }
}