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
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login);
            UserDialogs.Init(this);
        }

        public override void OnBackPressed()
        {
           
        }
    }
}