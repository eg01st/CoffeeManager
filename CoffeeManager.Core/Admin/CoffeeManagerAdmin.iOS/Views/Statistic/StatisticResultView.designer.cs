// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
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

            if (CrediCardTableView != null) {
                CrediCardTableView.Dispose ();
                CrediCardTableView = null;
            }

            if (CreditCardEntireAmountLabel != null) {
                CreditCardEntireAmountLabel.Dispose ();
                CreditCardEntireAmountLabel = null;
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