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
    [Register ("OrdersView")]
    partial class OrdersView
    {
        [Outlet]
        UIKit.UIButton CreateNewOrderButton { get; set; }


        [Outlet]
        UIKit.UITableView OrdersTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CreateNewOrderButton != null) {
                CreateNewOrderButton.Dispose ();
                CreateNewOrderButton = null;
            }

            if (OrdersTable != null) {
                OrdersTable.Dispose ();
                OrdersTable = null;
            }
        }
    }
}