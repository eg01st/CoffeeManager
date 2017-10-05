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
    [Register ("AddExpenseTypeView")]
    partial class AddExpenseTypeView
    {
        [Outlet]
        UIKit.UIButton AddButton { get; set; }


        [Outlet]
        UIKit.UITextField NameTextView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AddButton != null) {
                AddButton.Dispose ();
                AddButton = null;
            }

            if (NameTextView != null) {
                NameTextView.Dispose ();
                NameTextView = null;
            }
        }
    }
}