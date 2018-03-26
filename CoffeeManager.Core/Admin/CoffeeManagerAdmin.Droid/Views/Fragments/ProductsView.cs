using Android.Runtime;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.Core.ViewModels;
using MobileCore.Droid.Fragments;
using MvvmCross.Droid.Views.Attributes;

namespace CoffeeManagerAdmin.Droid.Views.Fragments
{

    [MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.home_tab_layout, ViewPagerResourceId = Resource.Id.home_viewpager, Title = nameof(ProductsView))]
    [Register(nameof(ProductsView))]
    public class ProductsView : FragmentBase<ProductsViewModel>
    {
        public ProductsView() : base(Resource.Layout.fragment_products)
        {
            
        } 
    }
}