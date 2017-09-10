using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using CoffeeManager.Core;
using CoffeeManager.Droid.Views;

namespace CoffeeManager.Droid
{
    [Activity(Theme = "@style/Theme.AppCompat.Light", Label = "Переучет", WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Portrait)]
    public class InventoryView : ActivityBase<InventoryViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.inventory);
            // Create your application here
        }
    }
}
