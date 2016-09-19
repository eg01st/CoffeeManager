using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using CoffeeManager.Core.ViewModels;
using CoffeeManager.Droid.Adapters;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using TabItem = CoffeeManager.Droid.Entities.TabItem;

namespace CoffeeManager.Droid.Views
{
    [Activity(Label = "MainView", Theme = "@style/Theme.AppCompat.Light")]
    public class MainView : ActivityBase<MainViewModel>
    {
        private ViewPager viewPager;
        private TabLayout tabLayout;

        private TabFactory tabFactory = new TabFactory();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main);
            // Create your application here

            viewPager = FindViewById<ViewPager>(Resource.Id.main_viewpager);
            tabLayout = FindViewById<TabLayout>(Resource.Id.main_tabs);

            SetTabLayout();
        }

        private void SetTabLayout()
        {
            var tabItems = tabFactory.Produce();
            SetupViewPager(tabItems);
            tabLayout.SetupWithViewPager(viewPager);

            for (var i = 0; i < tabItems.Length; i++)
            {
                var tab = tabLayout.GetTabAt(i);
                var tabItem = tabItems[i];
                tab.SetText(tabItem.Title);
            }
        }

        private void SetupViewPager(IEnumerable<TabItem> tabItems)
        {
            var adapter = new ViewPagerAdapter(SupportFragmentManager);
            foreach (var tabItem in tabItems)
            {
                var fragment = tabItem.Fragment;
                var title = tabItem.Title;
                adapter.AddFragment(fragment, title);
            }

            viewPager.Adapter = adapter;
        }

        public override void OnBackPressed()
        {
            
        }
    }
}