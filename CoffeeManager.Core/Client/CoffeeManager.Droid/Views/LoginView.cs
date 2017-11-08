using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using CoffeeManager.Core.ViewModels;
    
namespace CoffeeManager.Droid.Views
{
    [Activity(Label = "", ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginView : ActivityBase<LoginViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
           // UserDialogs.Init(this);

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login);
        }

        protected override void DoOnCreate()
        {
            
        }

        public override void OnBackPressed()
        {

        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            return false;
        }
    }
}