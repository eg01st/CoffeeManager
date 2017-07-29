// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS
{
    [Register ("ShiftDetailsView")]
    partial class ShiftDetailsView
    {
        [Outlet]
        UIKit.UILabel CoffeeCounter { get; set; }


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
        UIKit.UILabel UtilizedSalesLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CoffeeCounter != null) {
                CoffeeCounter.Dispose ();
                CoffeeCounter = null;
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
        }
    }
}