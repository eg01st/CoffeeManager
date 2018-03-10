using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using CoffeeManager.Core.ViewModels;
using MobileCore.Droid.Activities;

namespace CoffeeManager.Droid.Views
{
    [Activity(WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class EndShiftView : ActivityWithToolbar<EndShiftViewModel>
    {
        protected override string GetToolbarTitle() => "Окончание смены";

        protected override int GetUpNavigationIconId() => Resource.Drawable.ic_arrow_back_white_24dp;

        public EndShiftView() : base(Resource.Layout.end_shift)
        {
        }

    }
}