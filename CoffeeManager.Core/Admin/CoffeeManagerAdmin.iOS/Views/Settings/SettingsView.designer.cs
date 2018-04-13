// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//

using System.CodeDom.Compiler;
using Foundation;

namespace CoffeeManagerAdmin.iOS.Views.Settings
{
	[Register ("SettingsView")]
	partial class SettingsView
	{
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

		[Outlet]
		UIKit.UIButton CountersButton { get; set; }
		
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

			if (CountersButton != null) {
				CountersButton.Dispose ();
				CountersButton = null;
			}
		}
	}
}
