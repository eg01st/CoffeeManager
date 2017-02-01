using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using CoffeeManager.Core.ViewModels;

namespace CoffeeManager.Droid.Views
{
    [Activity(Label = "", Theme = "@style/Theme.AppCompat.Light", ScreenOrientation = ScreenOrientation.Landscape)]
    public class LoginView : ActivityBase<LoginViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            UserDialogs.Init(this);

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login);
        }

        public override void OnBackPressed()
        {

        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ViewModel.HandleError(e.ExceptionObject.ToString());
        }
    }
}