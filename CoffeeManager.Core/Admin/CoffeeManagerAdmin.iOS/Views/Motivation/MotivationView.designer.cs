// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Motivation
{
    [Register ("MotivationView")]
    partial class MotivationView
    {
        [Outlet]
        UIKit.UIButton FinishMotivationButton { get; set; }


        [Outlet]
        UIKit.UILabel MotivationStartDateLabel { get; set; }


        [Outlet]
        UIKit.UITableView MotivationTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (FinishMotivationButton != null) {
                FinishMotivationButton.Dispose ();
                FinishMotivationButton = null;
            }

            if (MotivationStartDateLabel != null) {
                MotivationStartDateLabel.Dispose ();
                MotivationStartDateLabel = null;
            }

            if (MotivationTableView != null) {
                MotivationTableView.Dispose ();
                MotivationTableView = null;
            }
        }
    }
}