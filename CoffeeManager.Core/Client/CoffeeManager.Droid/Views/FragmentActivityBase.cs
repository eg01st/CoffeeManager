using Android.App;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Views;

namespace CoffeeManager.Droid.Views
{
    [Activity]
    public class FragmentActivityBase <T> : MvxFragmentActivity<T> where T : ViewModelBase
    {
        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);
            DoOnCreate();
        } 

        protected virtual void DoOnCreate()
        {
            ActionBar?.SetDisplayHomeAsUpEnabled(true);
            ActionBar?.SetHomeButtonEnabled(true);
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