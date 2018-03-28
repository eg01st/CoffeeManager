using Android.App;
using Android.Content.PM;
using Android.Views;
using CoffeeManagerAdmin.Core.ViewModels.Users;
using MobileCore.Droid.Activities;
using MvvmCross.Droid.Views.Attributes;

namespace CoffeeManagerAdmin.Droid.Views.Users
{
    [MvxActivityPresentation]
    [Activity(WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Portrait)]
    public class UserDetailView: ActivityWithToolbar<UserDetailsViewModel>
    {
        protected override int GetMenuResourceId() => Resource.Menu.user_details_menu;

        protected override int GetToolbarTitleStringResourceId() => Resource.String.user_detail;

        protected override int GetUpNavigationIconId() => Resource.Drawable.ic_arrow_back_white_24dp;

        
        public UserDetailView() : base(Resource.Layout.user_detail)
        {
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.save)
            {
                ViewModel.UpdateCommand.Execute(null);
                return true;
            }
            
            return base.OnOptionsItemSelected(item);
        }
    }
}