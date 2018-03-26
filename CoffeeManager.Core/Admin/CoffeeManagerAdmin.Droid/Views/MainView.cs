using System;
using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views.Attributes;
using MobileCore.Droid.Activities;
using CoffeeManagerAdmin.Core.ViewModels;
using System.Collections.Generic;
using MobileCore.Droid.Bindings.CustomAtts;
using Android.Widget;
using Android.Support.V4.View;

namespace CoffeeManagerAdmin.Droid
{
    [MvxActivityPresentation]
    [Activity(ScreenOrientation = ScreenOrientation.Portrait, Label = "MainActivity", Theme = "@style/AppTheme", WindowSoftInputMode = Android.Views.SoftInput.AdjustNothing)]
    public class MainView : ActivityWithToolbar<MainViewModel>
    {
        private const int ViewPagerOffscreenPageLimit = 4;

        private readonly IList<int> tabIcons = new List<int>
        {
            //Resource.Drawable.news_tab_selector,
            //Resource.Drawable.topics_tab_selector,
            //Resource.Drawable.profile_tab_selector,
            //Resource.Drawable.more_tab_selector
        };

        private readonly IList<int> tabTitles = new List<int>
        {
            //Resource.String.fragment_news,
            //Resource.String.fragment_topics,
            //Resource.String.fragment_poeple,
            //Resource.String.fragment_more
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
