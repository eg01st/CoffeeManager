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
	[Register ("SelectOrderItemsView")]
	partial class SelectOrderItemsView
	{
		[Outlet]
		UIKit.UIButton AddProdButton { get; set; }

		[Outlet]
		UIKit.UIButton DoneButton { get; set; }

		[Outlet]
		UIKit.UITextField NewProductText { get; set; }

		[Outlet]
		UIKit.UITableView ProductsTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddProdButton != null) {
				AddProdButton.Dispose ();
				AddProdButton = null;
			}

			if (DoneButton != null) {
				DoneButton.Dispose ();
				DoneButton = null;
			}

			if (NewProductText != null) {
				NewProductText.Dispose ();
				NewProductText = null;
			}

			if (ProductsTableView != null) {
				ProductsTableView.Dispose ();
				ProductsTableView = null;
			}
		}
	}
}
