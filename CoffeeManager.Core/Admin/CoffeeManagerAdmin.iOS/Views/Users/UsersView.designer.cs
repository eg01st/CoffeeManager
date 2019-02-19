// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Users
{
    [Register ("UsersView")]
    partial class UsersView
    {
        [Outlet]
        UIKit.UILabel AmountForSalaryPayLabel { get; set; }


        [Outlet]
        UIKit.UITableView UsersTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AmountForSalaryPayLabel != null) {
                AmountForSalaryPayLabel.Dispose ();
                AmountForSalaryPayLabel = null;
            }

            if (UsersTableView != null) {
                UsersTableView.Dispose ();
                UsersTableView = null;
            }
        }
    }
}