// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Settings
{
    [Register ("SettingsView")]
    partial class SettingsView
    {
        [Outlet]
        UIKit.UIButton CountersButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AddCoffeeRoomButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView ClientsTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView CoffeeRoomsTable { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField CoffeeRoonNameTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AddCoffeeRoomButton != null) {
                AddCoffeeRoomButton.Dispose ();
                AddCoffeeRoomButton = null;
            }

            if (ClientsTableView != null) {
                ClientsTableView.Dispose ();
                ClientsTableView = null;
            }

            if (CoffeeRoomsTable != null) {
                CoffeeRoomsTable.Dispose ();
                CoffeeRoomsTable = null;
            }

            if (CoffeeRoonNameTextField != null) {
                CoffeeRoonNameTextField.Dispose ();
                CoffeeRoonNameTextField = null;
            }
        }
    }
}