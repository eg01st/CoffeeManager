
using Android.App;
using Android.Content.PM;
using Android.OS;
using CoffeeManager.Core.ViewModels;
using MobileCore.Droid.Activities;

namespace CoffeeManager.Droid.Views
{
    [Activity(ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class CurrentShiftExpensesView : ActivityWithToolbar<CurrentShiftExpensesViewModel>
    {
        protected override string GetToolbarTitle() => "Траты за смену";

        protected override int GetUpNavigationIconId() => Resource.Drawable.ic_arrow_back_white_24dp;

        public CurrentShiftExpensesView() : base(Resource.Layout.shift_expenses)
        {
        }
        
    }
}