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
    [Register ("InventoryView")]
    partial class InventoryView
    {
        [Outlet]
        UIKit.UITableView ReportsTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ReportsTableView != null) {
                ReportsTableView.Dispose ();
                ReportsTableView = null;
            }
        }
    }
}