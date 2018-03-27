using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.Support.V4.View;
using Android.Widget;
using CoffeeManagerAdmin.Core.ViewModels;
using MobileCore.Droid.Activities;
using MobileCore.Droid.Bindings.CustomAtts;
using MvvmCross.Droid.Views.Attributes;

namespace CoffeeManagerAdmin.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(ScreenOrientation = ScreenOrientation.Portrait,  WindowSoftInputMode = Android.Views.SoftInput.AdjustNothing)]
    public class MainView : ActivityBase<MainViewModel>
    {
        private const int ViewPagerOffscreenPageLimit = 5;

        private readonly IList<int> tabIcons = new List<int>
        {
            Resource.Drawable.finance_tab_selector,
            Resource.Drawable.storage_tab_selector,
            Resource.Drawable.expenses_tab_selector,
            Resource.Drawable.products_tab_selector,
            Resource.Drawable.statistic_tab_selector
        };

        private readonly IList<int> tabTitles = new List<int>
        {
            Resource.String.finance_tab_title,
            Resource.String.store_tab_title,
            Resource.String.expenses_tab_title,
            Resource.String.products_tab_title,
            Resource.String.statistic_tab_title,
        };

        [FindById(Resource.Id.home_tab_layout)]
        private Android.Support.Design.Widget.TabLayout TabLayout { get; set; }

        [FindById(Resource.Id.home_viewpager)]
        private ViewPager ViewPager { get; set; }

        public MainView()
            : base(Resource.Layout.main)
        {
        }

        public override void OnBackPressed()
        {
            MoveTaskToBack(true);
        }

      //  protected override bool GetUpNavigationEnabled() => false;

        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);

            ViewPager.OffscreenPageLimit = ViewPagerOffscreenPageLimit;

            if (bundle == null)
            {
                ViewModel.ShowInitialViewModelsAsync();
            }

            InitCustomTabs();
        }

        private void InitCustomTabs()
        {
            for (var i = 0; i < tabIcons.Count; i++)
            {
                var customTabView = LayoutInflater.Inflate(Resource.Layout.tab_item_main, null);
                customTabView.FindViewById<ImageView>(Resource.Id.tab_icon).SetImageResource(tabIcons[i]);
                customTabView.FindViewById<TextView>(Resource.Id.tab_title).Text = GetText(tabTitles[i]);

                var tab = TabLayout.GetTabAt(i);

                tab?.SetCustomView(customTabView);
            }
        }
    }
}
