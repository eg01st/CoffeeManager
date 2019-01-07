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
    [Register ("AutoOrderItemViewCell")]
    partial class AutoOrderItemViewCell
    {
        [Outlet]
        UIKit.UILabel DayOfWeekLabel { get; set; }


        [Outlet]
        UIKit.UILabel HourLabel { get; set; }


        [Outlet]
        UIKit.UISwitch IsActiveSwitch { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DayOfWeekLabel != null) {
                DayOfWeekLabel.Dispose ();
                DayOfWeekLabel = null;
            }

            if (HourLabel != null) {
                HourLabel.Dispose ();
                HourLabel = null;
            }

            if (IsActiveSwitch != null) {
                IsActiveSwitch.Dispose ();
                IsActiveSwitch = null;
            }
        }
    }
}