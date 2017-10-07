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
    [Register ("UtilizeView")]
    partial class UtilizeView
    {
        [Outlet]
        UIKit.UITableView UtilizedTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (UtilizedTableView != null) {
                UtilizedTableView.Dispose ();
                UtilizedTableView = null;
            }
        }
    }
}