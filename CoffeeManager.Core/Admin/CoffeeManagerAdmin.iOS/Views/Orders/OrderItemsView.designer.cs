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
	[Register ("OrderItemsView")]
	partial class OrderItemsView
	{
		[Outlet]
		UIKit.UIButton AddProductButton { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint AddSuplyProductBottomHeightConstraint { get; set; }

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
			if (AddProductButton != null) {
				AddProductButton.Dispose ();
				AddProductButton = null;
			}

			if (ExpenseTypeText != null) {
				ExpenseTypeText.Dispose ();
				ExpenseTypeText = null;
			}

			if (OrdersTableView != null) {
				OrdersTableView.Dispose ();
				OrdersTableView = null;
			}

			if (PriceLabel != null) {
				PriceLabel.Dispose ();
				PriceLabel = null;
			}

			if (StatusLabel != null) {
				StatusLabel.Dispose ();
				StatusLabel = null;
			}

			if (AddSuplyProductBottomHeightConstraint != null) {
				AddSuplyProductBottomHeightConstraint.Dispose ();
				AddSuplyProductBottomHeightConstraint = null;
			}
		}
	}
}
