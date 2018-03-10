using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using CoffeeManager.Core.ViewModels.UtilizedProducts;
using MobileCore.Droid.Activities;

namespace CoffeeManager.Droid.Views
{
    [Activity(WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class UtilizeProductsView : ActivityWithToolbar<UtilizeProductsViewModel>
    {
        protected override string GetToolbarTitle() => "Списание";

        protected override int GetUpNavigationIconId() => Resource.Drawable.ic_arrow_back_white_24dp;

        public UtilizeProductsView() : base(Resource.Layout.utilize_layout)
        {
        }
    }
}
