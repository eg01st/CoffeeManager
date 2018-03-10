using Android.App;
using Android.Content.PM;
using Android.Views;
using CoffeeManager.Core.ViewModels.Settings;
using MobileCore.Droid.Activities;

namespace CoffeeManager.Droid.Views
{
    [Activity(WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class SettingsView : ActivityWithToolbar<SettingsViewModel>
    {
        protected override string GetToolbarTitle() => "Настройки";

        protected override int GetUpNavigationIconId() => Resource.Drawable.ic_arrow_back_white_24dp;

        public SettingsView() : base(Resource.Layout.settings)
        {
        }
    }
}
