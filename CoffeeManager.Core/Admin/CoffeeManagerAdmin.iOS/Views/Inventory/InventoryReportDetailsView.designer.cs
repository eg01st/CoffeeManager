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
    [Register ("InventoryReportDetailsView")]
    partial class InventoryReportDetailsView
    {
        [Outlet]
        UIKit.UITableView ReportDetailsTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ReportDetailsTableView != null) {
                ReportDetailsTableView.Dispose ();
                ReportDetailsTableView = null;
            }
        }
    }
}