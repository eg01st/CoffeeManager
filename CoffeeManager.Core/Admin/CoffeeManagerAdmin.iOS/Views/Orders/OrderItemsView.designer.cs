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
	[Register ("OrderItemsView")]
	partial class OrderItemsView
	{
		[Outlet]
		UIKit.UIButton AddProductButton { get; set; }

		[Outlet]
		UIKit.UIButton CloseOrderButton { get; set; }

		[Outlet]
		UIKit.UITextField ExpenseTypeText { get; set; }

		[Outlet]
		UIKit.UITableView OrdersTableView { get; set; }

		[Outlet]
		UIKit.UILabel PriceLabel { get; set; }

		[Outlet]
		UIKit.UILabel StatusLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CloseOrderButton != null) {
				CloseOrderButton.Dispose ();
				CloseOrderButton = null;
			}

			if (AddProductButton != null) {
				AddProductButton.Dispose ();
				AddProductButton = null;
			}

			if (OrdersTableView != null) {
				OrdersTableView.Dispose ();
				OrdersTableView = null;
			}

			if (ExpenseTypeText != null) {
				ExpenseTypeText.Dispose ();
				ExpenseTypeText = null;
			}

			if (StatusLabel != null) {
				StatusLabel.Dispose ();
				StatusLabel = null;
			}

			if (PriceLabel != null) {
				PriceLabel.Dispose ();
				PriceLabel = null;
			}
		}
	}
}
