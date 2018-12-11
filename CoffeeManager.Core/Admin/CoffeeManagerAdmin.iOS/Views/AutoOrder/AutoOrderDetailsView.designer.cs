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
	[Register ("AutoOrderDetailsView")]
	partial class AutoOrderDetailsView
	{
		[Outlet]
		UIKit.UITableView OrderItemsTableView { get; set; }

		[Outlet]
		UIKit.UILabel OrderTime { get; set; }

		[Outlet]
		UIKit.UILabel OrderWeekDay { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (OrderWeekDay != null) {
				OrderWeekDay.Dispose ();
				OrderWeekDay = null;
			}

			if (OrderTime != null) {
				OrderTime.Dispose ();
				OrderTime = null;
			}

			if (OrderItemsTableView != null) {
				OrderItemsTableView.Dispose ();
				OrderItemsTableView = null;
			}
		}
	}
}
