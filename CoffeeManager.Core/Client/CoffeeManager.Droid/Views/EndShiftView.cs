using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using CoffeeManager.Core.ViewModels;

namespace CoffeeManager.Droid.Views
{
    [Activity(Theme = "@style/Theme.AppCompat.Light", Label = "Завершение смены", WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Landscape)]
    public class EndShiftView : ActivityBase<EndShiftViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.end_shift);
        }

        public override void OnBackPressed()
        {
            
        }
    }
}