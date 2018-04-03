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
	[Register ("CategoriesView")]
	partial class CategoriesView
	{
		[Outlet]
		UIKit.UITableView CategoriesTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CategoriesTableView != null) {
				CategoriesTableView.Dispose ();
				CategoriesTableView = null;
			}
		}
	}
}
