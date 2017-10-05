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

            if (AddSuplyProductBottomHeightConstraint != null) {
                AddSuplyProductBottomHeightConstraint.Dispose ();
                AddSuplyProductBottomHeightConstraint = null;
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
        }
    }
}