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
	[Register ("UtilizeView")]
	partial class UtilizeView
	{
		[Outlet]
		UIKit.UITableView UtilizedTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (UtilizedTableView != null) {
				UtilizedTableView.Dispose ();
				UtilizedTableView = null;
			}
		}
	}
}
