using Android.App;
using CoffeeManager.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace CoffeeManager.Droid.Views
{
    [Activity(Theme = "@style/Theme.AppCompat.Light")]
    public class ActivityBase <T> : MvxAppCompatActivity<T> where T : ViewModelBase
    {
    }
}