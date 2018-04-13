// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//

using Foundation;

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
			if (UsersTableView != null) {
				UsersTableView.Dispose ();
				UsersTableView = null;
			}

			if (AmountForSalaryPayLabel != null) {
				AmountForSalaryPayLabel.Dispose ();
				AmountForSalaryPayLabel = null;
			}
		}
	}
}
