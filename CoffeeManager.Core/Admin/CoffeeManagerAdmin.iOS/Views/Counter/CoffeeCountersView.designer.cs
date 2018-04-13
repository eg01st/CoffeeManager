// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//

using Foundation;

namespace CoffeeManagerAdmin.iOS.Views.Counter
{
	[Register ("CoffeeCountersView")]
	partial class CoffeeCountersView
	{
		[Outlet]
		UIKit.UITableView CountersTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CountersTableView != null) {
				CountersTableView.Dispose ();
				CountersTableView = null;
			}
		}
	}
}
