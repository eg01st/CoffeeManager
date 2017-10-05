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
    [Register ("SelectOrderItemsView")]
    partial class SelectOrderItemsView
    {
        [Outlet]
        UIKit.UIButton AddProdButton { get; set; }


        [Outlet]
        UIKit.UIButton DoneButton { get; set; }


        [Outlet]
        UIKit.UITextField NewProductText { get; set; }


        [Outlet]
        UIKit.UITableView ProductsTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ProductsTableView != null) {
                ProductsTableView.Dispose ();
                ProductsTableView = null;
            }
        }
    }
}