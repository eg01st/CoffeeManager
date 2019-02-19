// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Shifts.Counters
{
    [Register ("ShiftCounterTableViewCell")]
    partial class ShiftCounterTableViewCell
    {
        [Outlet]
        UIKit.UILabel DiffLabel { get; set; }


        [Outlet]
        UIKit.UILabel FinishLabel { get; set; }


        [Outlet]
        UIKit.UILabel NameLabel { get; set; }


        [Outlet]
        UIKit.UILabel StartLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DiffLabel != null) {
                DiffLabel.Dispose ();
                DiffLabel = null;
            }

            if (FinishLabel != null) {
                FinishLabel.Dispose ();
                FinishLabel = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }

            if (StartLabel != null) {
                StartLabel.Dispose ();
                StartLabel = null;
            }
        }
    }
}