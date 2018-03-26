using Android.Runtime;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.Core.ViewModels;
using MobileCore.Droid.Fragments;
using MvvmCross.Droid.Views.Attributes;

namespace CoffeeManagerAdmin.Droid.Views.Fragments
{
    [MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.home_tab_layout, ViewPagerResourceId = Resource.Id.home_viewpager, ActivityHostViewModelType = typeof(MainViewModel), Title = nameof(StoreFragment))]
    [Register(nameof(StoreFragment))]
    public class StoreFragment : FragmentBase<StorageViewModel>
    {
        public StoreFragment() : base(Resource.Layout.fragment_store)
        {
            
        } 
    }
}