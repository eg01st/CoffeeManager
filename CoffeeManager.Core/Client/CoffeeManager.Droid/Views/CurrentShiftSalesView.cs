using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using CoffeeManager.Core.ViewModels;
using MobileCore.Droid.Activities;

namespace CoffeeManager.Droid.Views
{
    [Activity(ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class CurrentShiftSalesView : ActivityWithToolbar<CurrentShiftSalesViewModel>
    {
        protected override string GetToolbarTitle() => "Продажи за смену";

        protected override int GetUpNavigationIconId() => Resource.Drawable.ic_arrow_back_white_24dp;

        public CurrentShiftSalesView() : base(Resource.Layout.current_sales)
        {
            
        }
    }
}