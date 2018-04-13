// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//

using Foundation;

namespace CoffeeManagerAdmin.iOS.Views.Home.SideMenu
{
	[Register ("SideMenuView")]
	partial class SideMenuView
	{
		[Outlet]
		UIKit.UITableView MenuTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MenuTableView != null) {
				MenuTableView.Dispose ();
				MenuTableView = null;
			}
		}
	}
}
