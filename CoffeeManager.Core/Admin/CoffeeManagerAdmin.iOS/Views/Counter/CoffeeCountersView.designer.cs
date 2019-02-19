// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Counter
{
    [Register ("CoffeeCountersView")]
    partial class CoffeeCountersView
    {
        [Outlet]
        UIKit.UITextField CoffeeRoomTextField { get; set; }


        [Outlet]
        UIKit.UITableView CountersTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CoffeeRoomTextField != null) {
                CoffeeRoomTextField.Dispose ();
                CoffeeRoomTextField = null;
            }

            if (CountersTableView != null) {
                CountersTableView.Dispose ();
                CountersTableView = null;
            }
        }
    }
}