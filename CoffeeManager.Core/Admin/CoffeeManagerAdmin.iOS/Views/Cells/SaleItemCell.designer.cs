// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS
{
    [Register ("SaleItemCell")]
    partial class SaleItemCell
    {
        [Outlet]
        UIKit.UILabel AmountLabel { get; set; }


        [Outlet]
        UIKit.UILabel NameLabel { get; set; }


        [Outlet]
        UIKit.UILabel RejectedLabel { get; set; }


        [Outlet]
        UIKit.UILabel TimeLabel { get; set; }


        [Outlet]
        UIKit.UILabel UtilizedLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AmountLabel != null) {
                AmountLabel.Dispose ();
                AmountLabel = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }

            if (RejectedLabel != null) {
                RejectedLabel.Dispose ();
                RejectedLabel = null;
            }

            if (TimeLabel != null) {
                TimeLabel.Dispose ();
                TimeLabel = null;
            }

            if (UtilizedLabel != null) {
                UtilizedLabel.Dispose ();
                UtilizedLabel = null;
            }
        }
    }
}