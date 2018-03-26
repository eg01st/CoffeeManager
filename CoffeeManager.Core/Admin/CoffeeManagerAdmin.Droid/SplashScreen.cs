using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace CoffeeManagerAdmin.Droid
{
    [Activity(MainLauncher = true,
              NoHistory = true,
              Theme = "@style/SplashTheme",
              Icon = "@mipmap/ic_launcher",
              ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}
