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
    [Register ("ExpenseTypeDetailsView")]
    partial class ExpenseTypeDetailsView
    {
        [Outlet]
        UIKit.UILabel ExpenseTypeNameLabel { get; set; }


        [Outlet]
        UIKit.UITableView MappedSuplyProductsTableView { get; set; }


        [Outlet]
        UIKit.UIButton MapSuplyProductButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ExpenseTypeNameLabel != null) {
                ExpenseTypeNameLabel.Dispose ();
                ExpenseTypeNameLabel = null;
            }

            if (MappedSuplyProductsTableView != null) {
                MappedSuplyProductsTableView.Dispose ();
                MappedSuplyProductsTableView = null;
            }

            if (MapSuplyProductButton != null) {
                MapSuplyProductButton.Dispose ();
                MapSuplyProductButton = null;
            }
        }
    }
}