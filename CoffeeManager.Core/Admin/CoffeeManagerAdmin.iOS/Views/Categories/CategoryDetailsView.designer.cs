// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Categories
{
    [Register ("CategoryDetailsView")]
    partial class CategoryDetailsView
    {
        [Outlet]
        UIKit.UIButton AddSubCategoryButton { get; set; }


        [Outlet]
        UIKit.UITextField NameTextField { get; set; }


        [Outlet]
        UIKit.UITableView SubCategoriesTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AddSubCategoryButton != null) {
                AddSubCategoryButton.Dispose ();
                AddSubCategoryButton = null;
            }

            if (NameTextField != null) {
                NameTextField.Dispose ();
                NameTextField = null;
            }

            if (SubCategoriesTableView != null) {
                SubCategoriesTableView.Dispose ();
                SubCategoriesTableView = null;
            }
        }
    }
}