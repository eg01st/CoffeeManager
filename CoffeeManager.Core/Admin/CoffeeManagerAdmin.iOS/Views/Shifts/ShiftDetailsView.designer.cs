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
	[Register ("ShiftDetailsView")]
	partial class ShiftDetailsView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton AddExpenseButton { get; set; }

		[Outlet]
		UIKit.UIButton CoffeeCountersButton { get; set; }

		[Outlet]
		UIKit.UILabel CoffeeSaleCounter { get; set; }

		[Outlet]
		UIKit.UILabel CopSalePercentageLabel { get; set; }

		[Outlet]
		UIKit.UILabel DateLabel { get; set; }

		[Outlet]
		UIKit.UITableView ExpenseTableView { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UILabel RejectedSalesLabel { get; set; }

		[Outlet]
		UIKit.UIButton SalesButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton UserNameButton { get; set; }

		[Outlet]
		UIKit.UILabel UtilizedSalesLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CoffeeCountersButton != null) {
				CoffeeCountersButton.Dispose ();
				CoffeeCountersButton = null;
			}

			if (CoffeeSaleCounter != null) {
				CoffeeSaleCounter.Dispose ();
				CoffeeSaleCounter = null;
			}

			if (CopSalePercentageLabel != null) {
				CopSalePercentageLabel.Dispose ();
				CopSalePercentageLabel = null;
			}

			if (DateLabel != null) {
				DateLabel.Dispose ();
				DateLabel = null;
			}

			if (ExpenseTableView != null) {
				ExpenseTableView.Dispose ();
				ExpenseTableView = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (RejectedSalesLabel != null) {
				RejectedSalesLabel.Dispose ();
				RejectedSalesLabel = null;
			}

			if (SalesButton != null) {
				SalesButton.Dispose ();
				SalesButton = null;
			}

			if (UtilizedSalesLabel != null) {
				UtilizedSalesLabel.Dispose ();
				UtilizedSalesLabel = null;
			}

			if (AddExpenseButton != null) {
				AddExpenseButton.Dispose ();
				AddExpenseButton = null;
			}

			if (UserNameButton != null) {
				UserNameButton.Dispose ();
				UserNameButton = null;
			}
		}
	}
}
