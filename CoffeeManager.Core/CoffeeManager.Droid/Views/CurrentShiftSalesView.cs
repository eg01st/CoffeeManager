using Android.App;
using Android.OS;
using CoffeeManager.Core.ViewModels;

namespace CoffeeManager.Droid.Views
{
    [Activity(Theme = "@style/Theme.AppCompat.Light", Label = "������� �� �����")]
    public class CurrentShiftSalesView : ActivityBase<CurrentShiftSalesViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.current_sales);
            // Create your application here
        }
    }
}