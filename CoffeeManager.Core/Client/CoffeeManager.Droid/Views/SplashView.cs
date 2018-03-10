using Android.App;
using Android.Content.PM;
using Android.OS;
using CoffeeManager.Core.ViewModels;

namespace CoffeeManager.Droid.Views
{
    [Activity(Theme = "@style/SplashTheme",
              NoHistory = true,
              ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class SplashView : ActivityBase<SplashViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SplashScreen);
            // Create your application here
        }
    }
}
