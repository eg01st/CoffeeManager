using Android.App;
using Android.Content.PM;
using CoffeeManagerAdmin.Core.ViewModels.Users;
using MobileCore.Droid.Activities;
using MvvmCross.Droid.Views.Attributes;

namespace CoffeeManagerAdmin.Droid.Views.Users
{
    [MvxActivityPresentation]
    [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
    public class UserEarningsView : ActivityWithToolbar<UserEarningsViewModel>
    {
        
    }
}