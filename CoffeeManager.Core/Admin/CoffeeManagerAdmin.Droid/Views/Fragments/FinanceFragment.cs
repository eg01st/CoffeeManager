using Android.Runtime;
using CoffeeManagerAdmin.Core.ViewModels;
using CoffeeManagerAdmin.Core.ViewModels.Home;
using MobileCore.Droid.Fragments;
using MvvmCross.Droid.Views.Attributes;

namespace CoffeeManagerAdmin.Droid.Views.Fragments
{
    [MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.home_tab_layout, ViewPagerResourceId = Resource.Id.home_viewpager, ActivityHostViewModelType = typeof(MainViewModel), Title = nameof(FinanceFragment))]
    [Register(nameof(FinanceFragment))]
    public class FinanceFragment : FragmentBase<MoneyViewModel>
    {
        public FinanceFragment() : base(Resource.Layout.fragment_finance)
        {
            
        } 
    }
}