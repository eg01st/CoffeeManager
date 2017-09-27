using Android.OS;
using Android.Views;
using CoffeManager.Common;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platform;

namespace CoffeeManager.Droid.Views.Fragments
{
    public class BaseFragment<T> : MvxFragment<T> where T: ViewModelBase
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.product_list, null);

            return view;
        }

    }
}