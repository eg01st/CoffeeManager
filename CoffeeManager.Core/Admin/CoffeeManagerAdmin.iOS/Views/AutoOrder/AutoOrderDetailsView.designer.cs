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
    [Register ("AutoOrderDetailsView")]
    partial class AutoOrderDetailsView
    {
        [Outlet]
        UIKit.UITableView OrderItemsTableView { get; set; }


        [Outlet]
        UIKit.UILabel OrderTime { get; set; }


        [Outlet]
        UIKit.UILabel OrderWeekDay { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (OrderItemsTableView != null) {
                OrderItemsTableView.Dispose ();
                OrderItemsTableView = null;
            }

            if (OrderTime != null) {
                OrderTime.Dispose ();
                OrderTime = null;
            }

            if (OrderWeekDay != null) {
                OrderWeekDay.Dispose ();
                OrderWeekDay = null;
            }
        }
    }
}