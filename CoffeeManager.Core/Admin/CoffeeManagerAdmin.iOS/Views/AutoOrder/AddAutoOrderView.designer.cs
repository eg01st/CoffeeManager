// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS
{
	[Register ("AddAutoOrderView")]
	partial class AddAutoOrderView
	{
		[Outlet]
		UIKit.UIButton AddSuplyProductsButton { get; set; }

		[Outlet]
		UIKit.UITextField HourTextField { get; set; }

		[Outlet]
		UIKit.UITableView SUplyProductsTableView { get; set; }

		[Outlet]
		UIKit.UITextField WeekDayTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (WeekDayTextField != null) {
				WeekDayTextField.Dispose ();
				WeekDayTextField = null;
			}

			if (HourTextField != null) {
				HourTextField.Dispose ();
				HourTextField = null;
			}

			if (AddSuplyProductsButton != null) {
				AddSuplyProductsButton.Dispose ();
				AddSuplyProductsButton = null;
			}

			if (SUplyProductsTableView != null) {
				SUplyProductsTableView.Dispose ();
				SUplyProductsTableView = null;
			}
		}
	}
}
