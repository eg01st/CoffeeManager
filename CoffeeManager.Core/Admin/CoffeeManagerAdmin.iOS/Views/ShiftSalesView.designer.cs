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
	[Register ("ShiftSalesView")]
	partial class ShiftSalesView
	{
		[Outlet]
		UIKit.UIButton ByProductButton { get; set; }

		[Outlet]
		UIKit.UIButton ByTimeButton { get; set; }

		[Outlet]
		UIKit.UITableView SalesTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ByTimeButton != null) {
				ByTimeButton.Dispose ();
				ByTimeButton = null;
			}

			if (ByProductButton != null) {
				ByProductButton.Dispose ();
				ByProductButton = null;
			}

			if (SalesTableView != null) {
				SalesTableView.Dispose ();
				SalesTableView = null;
			}
		}
	}
}
