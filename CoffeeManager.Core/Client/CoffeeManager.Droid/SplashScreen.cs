
using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace CoffeeManager.Droid
{
    [Activity(Label = "Coffee Manager", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen() : base(Resource.Layout.SplashScreen)
        {
            
        }
    }
}