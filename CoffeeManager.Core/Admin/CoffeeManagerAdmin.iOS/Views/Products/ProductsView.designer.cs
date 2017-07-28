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
	[Register ("ProductsView")]
	partial class ProductsView
	{
		[Outlet]
		UIKit.UIButton AddProductButton { get; set; }

		[Outlet]
		UIKit.UITextField CupTypeCategoryText { get; set; }

		[Outlet]
		UIKit.UIButton EditProductButton { get; set; }

		[Outlet]
		UIKit.UITextField NameText { get; set; }

		[Outlet]
		UIKit.UITextField PolicePriceText { get; set; }

		[Outlet]
		UIKit.UITextField PriceText { get; set; }

		[Outlet]
		UIKit.UITableView ProductsTableView { get; set; }

		[Outlet]
		UIKit.UITextField ProductTypeText { get; set; }

		[Outlet]
		UIKit.UITextField SuplyBindingLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SuplyBindingLabel != null) {
				SuplyBindingLabel.Dispose ();
				SuplyBindingLabel = null;
			}

			if (AddProductButton != null) {
				AddProductButton.Dispose ();
				AddProductButton = null;
			}

			if (CupTypeCategoryText != null) {
				CupTypeCategoryText.Dispose ();
				CupTypeCategoryText = null;
			}

			if (EditProductButton != null) {
				EditProductButton.Dispose ();
				EditProductButton = null;
			}

			if (NameText != null) {
				NameText.Dispose ();
				NameText = null;
			}

			if (PolicePriceText != null) {
				PolicePriceText.Dispose ();
				PolicePriceText = null;
			}

			if (PriceText != null) {
				PriceText.Dispose ();
				PriceText = null;
			}

			if (ProductsTableView != null) {
				ProductsTableView.Dispose ();
				ProductsTableView = null;
			}

			if (ProductTypeText != null) {
				ProductTypeText.Dispose ();
				ProductTypeText = null;
			}
		}
	}
}
