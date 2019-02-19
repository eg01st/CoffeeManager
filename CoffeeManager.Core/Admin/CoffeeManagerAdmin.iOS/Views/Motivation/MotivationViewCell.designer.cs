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
    [Register ("MotivationViewCell")]
    partial class MotivationViewCell
    {
        [Outlet]
        UIKit.UILabel EntireScoreLabel { get; set; }


        [Outlet]
        UIKit.UILabel MoneyScoreLabel { get; set; }


        [Outlet]
        UIKit.UILabel NameLabel { get; set; }


        [Outlet]
        UIKit.UILabel ShiftScoreLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (EntireScoreLabel != null) {
                EntireScoreLabel.Dispose ();
                EntireScoreLabel = null;
            }

            if (MoneyScoreLabel != null) {
                MoneyScoreLabel.Dispose ();
                MoneyScoreLabel = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }

            if (ShiftScoreLabel != null) {
                ShiftScoreLabel.Dispose ();
                ShiftScoreLabel = null;
            }
        }
    }
}