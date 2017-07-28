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
	[Register ("MainView")]
	partial class MainView
	{
		[Outlet]
		UIKit.UILabel CurrentAmountLabel { get; set; }

		[Outlet]
		UIKit.UILabel CurrentShiftAmountLabel { get; set; }

		[Outlet]
		UIKit.UIButton ExpensesButton { get; set; }

		[Outlet]
		UIKit.UIButton OrdersButton { get; set; }

		[Outlet]
		UIKit.UIButton ProductCalculationButton { get; set; }

		[Outlet]
		UIKit.UIButton ProductsButton { get; set; }

		[Outlet]
		UIKit.UIButton ShiftButton { get; set; }

		[Outlet]
		UIKit.UIButton StatisticButton { get; set; }

		[Outlet]
		UIKit.UIButton SupliedProductsButton { get; set; }

		[Outlet]
		UIKit.UIButton UpdateButton { get; set; }

		[Outlet]
		UIKit.UIButton UsersButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CurrentAmountLabel != null) {
				CurrentAmountLabel.Dispose ();
				CurrentAmountLabel = null;
			}

			if (CurrentShiftAmountLabel != null) {
				CurrentShiftAmountLabel.Dispose ();
				CurrentShiftAmountLabel = null;
			}

			if (OrdersButton != null) {
				OrdersButton.Dispose ();
				OrdersButton = null;
			}

			if (ProductCalculationButton != null) {
				ProductCalculationButton.Dispose ();
				ProductCalculationButton = null;
			}

			if (ProductsButton != null) {
				ProductsButton.Dispose ();
				ProductsButton = null;
			}

			if (ShiftButton != null) {
				ShiftButton.Dispose ();
				ShiftButton = null;
			}

			if (StatisticButton != null) {
				StatisticButton.Dispose ();
				StatisticButton = null;
			}

			if (SupliedProductsButton != null) {
				SupliedProductsButton.Dispose ();
				SupliedProductsButton = null;
			}

			if (UpdateButton != null) {
				UpdateButton.Dispose ();
				UpdateButton = null;
			}

			if (UsersButton != null) {
				UsersButton.Dispose ();
				UsersButton = null;
			}

			if (ExpensesButton != null) {
				ExpensesButton.Dispose ();
				ExpensesButton = null;
			}
		}
	}
}
