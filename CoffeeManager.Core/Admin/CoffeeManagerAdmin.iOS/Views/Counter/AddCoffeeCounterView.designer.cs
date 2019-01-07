// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Counter
{
    [Register ("AddCoffeeCounterView")]
    partial class AddCoffeeCounterView
    {
        [Outlet]
        UIKit.UITextField CategoryTextField { get; set; }


        [Outlet]
        UIKit.UITextField NameTextField { get; set; }


        [Outlet]
        UIKit.UIButton SelectCategoryButton { get; set; }


        [Outlet]
        UIKit.UIButton SelectSuplyProductButton { get; set; }


        [Outlet]
        UIKit.UITextField SuplyProductTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CategoryTextField != null) {
                CategoryTextField.Dispose ();
                CategoryTextField = null;
            }

            if (NameTextField != null) {
                NameTextField.Dispose ();
                NameTextField = null;
            }

            if (SelectCategoryButton != null) {
                SelectCategoryButton.Dispose ();
                SelectCategoryButton = null;
            }

            if (SelectSuplyProductButton != null) {
                SelectSuplyProductButton.Dispose ();
                SelectSuplyProductButton = null;
            }

            if (SuplyProductTextField != null) {
                SuplyProductTextField.Dispose ();
                SuplyProductTextField = null;
            }
        }
    }
}