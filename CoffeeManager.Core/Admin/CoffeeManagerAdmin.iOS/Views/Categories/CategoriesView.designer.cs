// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Categories
{
    [Register ("CategoriesView")]
    partial class CategoriesView
    {
        [Outlet]
        UIKit.UITableView CategoriesTableView { get; set; }


        [Outlet]
        UIKit.UITextField CoffeeRoomTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CategoriesTableView != null) {
                CategoriesTableView.Dispose ();
                CategoriesTableView = null;
            }

            if (CoffeeRoomTextField != null) {
                CoffeeRoomTextField.Dispose ();
                CoffeeRoomTextField = null;
            }
        }
    }
}