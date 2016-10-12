using Android.App;
using Android.Content.PM;
using Android.OS;
using CoffeeManager.Core.ViewModels;

namespace CoffeeManager.Droid.Views
{
    [Activity(Theme = "@style/Theme.AppCompat.Light", Label = "Errors", ScreenOrientation = ScreenOrientation.Landscape)]
    public class ErrorsView : ActivityBase<ErrorsViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.errors);
        }
    }
}