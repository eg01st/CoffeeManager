// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS
{
    [Register("SelectOrderItemViewCell")]
    partial class SelectOrderItemViewCell
    {
        [Outlet]
        UIKit.UILabel CountLabel { get; set; }

        [Outlet]
        UIKit.UISwitch IsSelected { get; set; }

        [Outlet]
        UIKit.UILabel NameLabel { get; set; }

        void ReleaseDesignerOutlets()
        {
            if (CountLabel != null)
            {
                CountLabel.Dispose();
                CountLabel = null;
            }

            if (NameLabel != null)
            {
                NameLabel.Dispose();
                NameLabel = null;
            }

            if (IsSelected != null)
            {
                IsSelected.Dispose();
                IsSelected = null;
            }
        }
    }
}
