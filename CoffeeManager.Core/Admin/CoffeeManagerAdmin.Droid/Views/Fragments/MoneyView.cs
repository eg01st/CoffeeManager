using Android.Runtime;
using CoffeeManagerAdmin.Core.ViewModels;
using CoffeeManagerAdmin.Core.ViewModels.Home;
using MobileCore.Droid.Fragments;
using MvvmCross.Droid.Views.Attributes;

namespace CoffeeManagerAdmin.Droid.Views.Fragments
{
    [MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.home_tab_layout, ViewPagerResourceId = Resource.Id.home_viewpager,Title = nameof(MoneyView))]
    [Register(nameof(MoneyView))]
    public class MoneyView : FragmentBase<MoneyViewModel>
    {
        public MoneyView() : base(Resource.Layout.fragment_finance)
        {
            
        } 
    }
}