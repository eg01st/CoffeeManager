// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Categories
{
	[Register ("CategoriesView")]
	partial class CategoriesView
	{
		[Outlet]
		UIKit.UITableView CategoriesTableView { get; set; }

		[Outlet]
		UIKit.UITextField CoffeeRoomTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CategoriesTableView != null) {
				CategoriesTableView.Dispose ();
				CategoriesTableView = null;
			}

			if (CoffeeRoomTextField != null) {
				CoffeeRoomTextField.Dispose ();
				CoffeeRoomTextField = null;
			}
		}
	}
}
