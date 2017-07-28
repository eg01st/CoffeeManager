// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS
{
	[Register ("OrdersView")]
	partial class OrdersView
	{
		[Outlet]
		UIKit.UIButton CreateNewOrderButton { get; set; }

		[Outlet]
		UIKit.UITableView OrdersTable { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (OrdersTable != null) {
				OrdersTable.Dispose ();
				OrdersTable = null;
			}

			if (CreateNewOrderButton != null) {
				CreateNewOrderButton.Dispose ();
				CreateNewOrderButton = null;
			}
		}
	}
}
