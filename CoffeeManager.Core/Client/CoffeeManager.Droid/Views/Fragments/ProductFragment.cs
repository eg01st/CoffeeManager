using Android.OS;
using Android.Views;
using CoffeManager.Common;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;

namespace CoffeeManager.Droid.Views.Fragments
{
    public class ProductFragment : MvxFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.product_list, null);

            return view;
        }
    }
}