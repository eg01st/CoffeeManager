using System;
using Android.App;
using Android.Content.PM;
using CoffeeManagerAdmin.Core.ViewModels;
using MobileCore.Droid.Activities;

namespace CoffeeManagerAdmin.Droid
{
    [Activity(ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class LoginView : ActivityBase<LoginViewModel>
    {
        public LoginView() : base(Resource.Layout.login)
        {
        }
    }
}
