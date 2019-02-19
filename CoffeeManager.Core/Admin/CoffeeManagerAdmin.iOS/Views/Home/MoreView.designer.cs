// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Home
{
    [Register ("MoreView")]
    partial class MoreView
    {
        [Outlet]
        UIKit.UITableView ItemsTableSource { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ItemsTableSource != null) {
                ItemsTableSource.Dispose ();
                ItemsTableSource = null;
            }
        }
    }
}