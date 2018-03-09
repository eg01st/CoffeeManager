using Android.App;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;

namespace CoffeeManager.Droid.Views
{
    [Activity]
    public class ActivityBase <T> : MvxAppCompatActivity<T> where T : ViewModelBase
    {
        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);
            DoOnCreate();
        } 

        protected virtual void DoOnCreate()
        {
            Toolbar toolbar = (Toolbar) FindViewById(Resource.Id.toolbar);
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
            }

            SupportActionBar?.SetCustomView(Resource.Layout.toolbar);
            SupportActionBar?.SetDisplayShowCustomEnabled(true);

          
            SupportActionBar?.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar?.SetHomeButtonEnabled(true);
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            var itemId = item.ItemId;
            if (itemId == Android.Resource.Id.Home)
            {
                ViewModel.CloseCommand.Execute(null);
                return true;
            }
            return false;
        }

        public override void OnBackPressed()
        {
            ViewModel.CloseCommand.Execute(null);
        }
    }
}