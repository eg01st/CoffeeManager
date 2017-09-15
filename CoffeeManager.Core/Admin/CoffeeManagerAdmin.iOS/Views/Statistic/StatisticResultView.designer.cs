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
	[Register ("StatisticResultView")]
	partial class StatisticResultView
	{
		[Outlet]
		UIKit.UIView ContainerView { get; set; }

		[Outlet]
		UIKit.UIView CreaditCardHeaderView { get; set; }

		[Outlet]
		UIKit.UITableView CrediCardTableView { get; set; }

		[Outlet]
		UIKit.UILabel CreditCardEntireAmountLabel { get; set; }

		[Outlet]
		UIKit.UIView CreditCardView { get; set; }

		[Outlet]
		UIKit.UIView ExpensesHeaderView { get; set; }

		[Outlet]
		UIKit.UITableView ExpensesTableView { get; set; }

		[Outlet]
		UIKit.UIView ExpensesView { get; set; }

		[Outlet]
		UIKit.UIView SalesHeaderView { get; set; }

		[Outlet]
		UIKit.UITableView SalesTableView { get; set; }

		[Outlet]
		UIKit.UIView SalesView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ContainerView != null) {
				ContainerView.Dispose ();
				ContainerView = null;
			}

			if (CreaditCardHeaderView != null) {
				CreaditCardHeaderView.Dispose ();
				CreaditCardHeaderView = null;
			}

			if (ExpensesHeaderView != null) {
				ExpensesHeaderView.Dispose ();
				ExpensesHeaderView = null;
			}

			if (SalesHeaderView != null) {
				SalesHeaderView.Dispose ();
				SalesHeaderView = null;
			}

			if (CreditCardView != null) {
				CreditCardView.Dispose ();
				CreditCardView = null;
			}

			if (CreditCardEntireAmountLabel != null) {
				CreditCardEntireAmountLabel.Dispose ();
				CreditCardEntireAmountLabel = null;
			}

			if (CrediCardTableView != null) {
				CrediCardTableView.Dispose ();
				CrediCardTableView = null;
			}

			if (ExpensesView != null) {
				ExpensesView.Dispose ();
				ExpensesView = null;
			}

			if (ExpensesTableView != null) {
				ExpensesTableView.Dispose ();
				ExpensesTableView = null;
			}

			if (SalesView != null) {
				SalesView.Dispose ();
				SalesView = null;
			}

			if (SalesTableView != null) {
				SalesTableView.Dispose ();
				SalesTableView = null;
			}
		}
	}
}
