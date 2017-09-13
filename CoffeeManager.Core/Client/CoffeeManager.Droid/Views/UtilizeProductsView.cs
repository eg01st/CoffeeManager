using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using CoffeeManager.Core;
using CoffeeManager.Droid.Views;

namespace CoffeeManager.Droid
{
    [Activity(Theme = "@style/Theme.AppCompat.Light", Label = "Списание", WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Portrait)]

    public class UtilizeProductsView : ActivityBase<UtilizeProductsViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.utilize_layout);
        // Create your application here
        }
    }
}
