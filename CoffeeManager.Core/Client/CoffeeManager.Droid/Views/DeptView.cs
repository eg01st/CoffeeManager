using Android.App;
using Android.Content.PM;
using Android.OS;
using CoffeeManager.Core.ViewModels;

namespace CoffeeManager.Droid.Views
{
    [Activity(Theme = "@style/Theme.AppCompat.Light", ScreenOrientation = ScreenOrientation.Landscape)]
    public class DeptView : ActivityBase<DeptViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.dept_layout);
            // Create your application here
        }
    }
}