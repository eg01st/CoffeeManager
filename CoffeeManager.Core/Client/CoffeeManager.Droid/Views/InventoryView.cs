using Android.App;
using Android.Content.PM;
using Android.Views;
using CoffeeManager.Core;
using CoffeeManager.Core.ViewModels.Inventory;
using MobileCore.Droid.Activities;

namespace CoffeeManager.Droid.Views
{
    [Activity(WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class InventoryView : ActivityWithToolbar<InventoryViewModel>
    {
        protected override string GetToolbarTitle() => "Переучет";

        protected override int GetUpNavigationIconId() => Resource.Drawable.ic_arrow_back_white_24dp;

        public InventoryView() : base(Resource.Layout.inventory)
        {
        }
    }
}
