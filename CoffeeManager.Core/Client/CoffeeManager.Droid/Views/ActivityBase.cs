using Android.App;
using CoffeManager.Common;
using MvvmCross.Droid.Support.V7.AppCompat;

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