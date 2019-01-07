// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Home
{
    [Register ("StatisticView")]
    partial class StatisticView
    {
        [Outlet]
        UIKit.UIButton DoneButton { get; set; }


        [Outlet]
        UIKit.UITextField FromTextField { get; set; }


        [Outlet]
        UIKit.UITextField ToTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DoneButton != null) {
                DoneButton.Dispose ();
                DoneButton = null;
            }

            if (FromTextField != null) {
                FromTextField.Dispose ();
                FromTextField = null;
            }

            if (ToTextField != null) {
                ToTextField.Dispose ();
                ToTextField = null;
            }
        }
    }
}