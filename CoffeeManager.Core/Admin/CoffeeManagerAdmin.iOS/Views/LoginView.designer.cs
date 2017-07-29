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
    [Register ("LoginView")]
    partial class LoginView
    {
        [Outlet]
        UIKit.UIButton LoginButton { get; set; }


        [Outlet]
        UIKit.UITextField LoginText { get; set; }


        [Outlet]
        UIKit.UITextField PasswordText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LoginButton != null) {
                LoginButton.Dispose ();
                LoginButton = null;
            }

            if (LoginText != null) {
                LoginText.Dispose ();
                LoginText = null;
            }

            if (PasswordText != null) {
                PasswordText.Dispose ();
                PasswordText = null;
            }
        }
    }
}