using Android.Runtime;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.Core.ViewModels;
using CoffeeManagerAdmin.Core.ViewModels.ManageExpenses;
using MobileCore.Droid.Fragments;
using MvvmCross.Droid.Views.Attributes;

namespace CoffeeManagerAdmin.Droid.Views.Fragments
{
    [MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.home_tab_layout, ViewPagerResourceId = Resource.Id.home_viewpager, Title = nameof(ManageExpensesView))]
    [Register(nameof(ManageExpensesView))]
    public class ManageExpensesView : FragmentBase<ManageExpensesViewModel>
    {
        public ManageExpensesView() : base(Resource.Layout.fragment_expenses)
        {
            
        } 
    }
}