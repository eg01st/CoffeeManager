
using Android.App;
using Android.Content.PM;
using Android.OS;
using CoffeeManager.Core.ViewModels;

namespace CoffeeManager.Droid.Views
{
    [Activity(Theme = "@style/Theme.AppCompat.Light", Label = "Траты за смену", ScreenOrientation = ScreenOrientation.Portrait)]

    public class CurrentShiftExpensesView : ActivityBase<CurrentShiftExpensesViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.shift_expenses);
            // Create your application here
        }
    }
}