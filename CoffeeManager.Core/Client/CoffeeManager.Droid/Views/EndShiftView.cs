using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using CoffeeManager.Core.ViewModels;

namespace CoffeeManager.Droid.Views
{
    [Activity(Theme = "@style/Theme.AppCompat.Light", Label = "Окончание смены", WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Portrait)]
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

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return false;
        }
    }
}