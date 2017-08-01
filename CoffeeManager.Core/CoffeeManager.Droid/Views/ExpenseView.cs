using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using CoffeeManager.Core.ViewModels;

namespace CoffeeManager.Droid.Views
{
    [Activity(Theme = "@style/Theme.AppCompat.Light", Label = "�������",WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Landscape)]
    public class ExpenseView : ActivityBase<ExpenseViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.exprense_layout);
            // Create your application here
        }
    }
}