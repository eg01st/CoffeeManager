// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS
{
    [Register ("UsersView")]
    partial class UsersView
    {
        [Outlet]
        UIKit.UIButton AddButton { get; set; }


        [Outlet]
        UIKit.UITextField NameText { get; set; }


        [Outlet]
        UIKit.UITableView UsersTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (UsersTableView != null) {
                UsersTableView.Dispose ();
                UsersTableView = null;
            }
        }
    }
}