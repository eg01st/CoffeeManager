// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Shifts.Counters
{
    [Register ("ShiftCountersView")]
    partial class ShiftCountersView
    {
        [Outlet]
        UIKit.UITableView CountersTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CountersTableView != null) {
                CountersTableView.Dispose ();
                CountersTableView = null;
            }
        }
    }
}