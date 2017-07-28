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
	[Register ("ProductsCalculationView")]
	partial class ProductsCalculationView
	{
		[Outlet]
		UIKit.UITableView ProductsTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ProductsTableView != null) {
				ProductsTableView.Dispose ();
				ProductsTableView = null;
			}
		}
	}
}
