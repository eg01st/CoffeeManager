using Android.Runtime;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.Core.ViewModels;
using MobileCore.Droid.Fragments;
using MvvmCross.Droid.Views.Attributes;

namespace CoffeeManagerAdmin.Droid.Views.Fragments
{

    [MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.home_tab_layout, ViewPagerResourceId = Resource.Id.home_viewpager, ActivityHostViewModelType = typeof(MainViewModel), Title = nameof(ProductsFragment))]
    [Register(nameof(ProductsFragment))]
    public class ProductsFragment : FragmentBase<ProductsViewModel>
    {
        public ProductsFragment() : base(Resource.Layout.fragment_products)
        {
            
        } 
    }
}