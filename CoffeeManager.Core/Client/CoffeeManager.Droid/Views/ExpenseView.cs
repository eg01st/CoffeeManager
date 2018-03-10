using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using CoffeeManager.Core.ViewModels;
using MobileCore.Droid.Activities;

namespace CoffeeManager.Droid.Views
{
    [Activity(WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class ExpenseView : ActivityWithToolbar<ExpenseViewModel>
    {
        protected override string GetToolbarTitle() => "Расходы";

        protected override int GetUpNavigationIconId() => Resource.Drawable.ic_arrow_back_white_24dp;

        public ExpenseView() : base(Resource.Layout.exprense_layout)
        {
        }
    }
}