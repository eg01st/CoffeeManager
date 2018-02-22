
using Android.App;
using Android.Content.PM;
using Android.OS;
using CoffeeManager.Core;
using CoffeeManager.Core.ViewModels;
using CoffeeManager.Droid.Views;

namespace CoffeeManager.Droid
{
    [Activity(Theme = "@style/SplashTheme",
              NoHistory = true,
              ScreenOrientation = ScreenOrientation.Portrait)]
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
