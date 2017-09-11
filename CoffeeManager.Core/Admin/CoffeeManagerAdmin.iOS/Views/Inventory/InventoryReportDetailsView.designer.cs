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
	[Register ("InventoryReportDetailsView")]
	partial class InventoryReportDetailsView
	{
		[Outlet]
		UIKit.UITableView ReportDetailsTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ReportDetailsTableView != null) {
				ReportDetailsTableView.Dispose ();
				ReportDetailsTableView = null;
			}
		}
	}
}
