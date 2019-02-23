// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Statistic
{
	[Register ("StatisticResultSubView")]
	partial class StatisticResultSubView
	{
		[Outlet]
		UIKit.UIView ContainerView { get; set; }

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

			if (CreditCardView != null) {
				CreditCardView.Dispose ();
				CreditCardView = null;
			}

			if (ExpensesHeaderView != null) {
				ExpensesHeaderView.Dispose ();
				ExpensesHeaderView = null;
			}

			if (ExpensesTableView != null) {
				ExpensesTableView.Dispose ();
				ExpensesTableView = null;
			}

			if (ExpensesView != null) {
				ExpensesView.Dispose ();
				ExpensesView = null;
			}

			if (SalesHeaderView != null) {
				SalesHeaderView.Dispose ();
				SalesHeaderView = null;
			}

			if (SalesTableView != null) {
				SalesTableView.Dispose ();
				SalesTableView = null;
			}

			if (SalesView != null) {
				SalesView.Dispose ();
				SalesView = null;
			}
		}
	}
}
