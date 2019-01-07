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
    [Register ("AutoOrderView")]
    partial class AutoOrderView
    {
        [Outlet]
        UIKit.UITableView OrdersTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (OrdersTableView != null) {
                OrdersTableView.Dispose ();
                OrdersTableView = null;
            }
        }
    }
}