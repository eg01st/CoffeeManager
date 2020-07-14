// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MobileCore.iOS
{
    [Register ("PageTitleViewCell")]
    partial class PageTitleViewCell
    {
        [Outlet]
        UIKit.UIView IndicatorView { get; set; }


        [Outlet]
        UIKit.UILabel PageTitleLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (IndicatorView != null) {
                IndicatorView.Dispose ();
                IndicatorView = null;
            }

            if (PageTitleLabel != null) {
                PageTitleLabel.Dispose ();
                PageTitleLabel = null;
            }
        }
    }
}