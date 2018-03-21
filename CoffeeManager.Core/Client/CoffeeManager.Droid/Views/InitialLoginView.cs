
using Android.App;
using Android.Content.PM;
using Android.OS;
using CoffeeManager.Core;
using CoffeeManager.Droid.Views;

namespace CoffeeManager.Droid
{
    [Activity(Theme = "@style/SplashTheme",
              NoHistory = true,
              ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class InitialLoginView : ActivityBase<InitialLoginViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.initial_login);
            // Create your application here
        }

        public override void OnBackPressed()
        {

        }
    }
}
