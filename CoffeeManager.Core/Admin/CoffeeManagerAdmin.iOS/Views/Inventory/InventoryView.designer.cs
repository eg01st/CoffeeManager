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
	[Register ("InventoryView")]
	partial class InventoryView
	{
		[Outlet]
		UIKit.UITableView ReportsTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ReportsTableView != null) {
				ReportsTableView.Dispose ();
				ReportsTableView = null;
			}
		}
	}
}
