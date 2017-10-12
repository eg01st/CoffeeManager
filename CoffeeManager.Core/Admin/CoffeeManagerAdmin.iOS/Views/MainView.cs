using System;
using Foundation;
using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;
using CoreGraphics;
using MvvmCross.Binding.iOS.Views;
using CoffeManager.Common;
using System.Collections.Generic;

namespace CoffeeManagerAdmin.iOS
{
    [Register("MainView")]
    public partial class MainView : MainTabController
    {
        private ExpandableTabBar tabbar;

        protected override void DoViewDidLoad()
        {
            base.DoViewDidLoad();

            InitTabBar();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationItem.SetHidesBackButton(true, false);
        }


        private void InitTabBar()
        {
            tabbar = new ExpandableTabBar(this);

         
            var moneytabBarItem = new ExpandableTabBar.ExpandableTabBarItem(0,"ic_attach_money_white.png" , "ic_attach_money.png", "Финансы");
            var storagetabBarItem = new ExpandableTabBar.ExpandableTabBarItem(1, "ic_shopping_basket_white.png", "ic_shopping_basket.png", "Склад");
            var userstabBarItem = new ExpandableTabBar.ExpandableTabBarItem(2, "ic_account_circle_white.png", "ic_account_circle.png", "Персонал");
            var statistictabBarItem = new ExpandableTabBar.ExpandableTabBarItem(3, "ic_trending_up_white.png", "ic_trending_up.png", "Статистика");

            var storagetabBarItem1 = new ExpandableTabBar.ExpandableTabBarItem(1, "ic_shopping_basket_white.png", "ic_shopping_basket.png", "Склад");
            var userstabBarItem1 = new ExpandableTabBar.ExpandableTabBarItem(2, "ic_account_circle_white.png", "ic_account_circle.png", "Персонал");


            tabbar.AddTabItems(new List<ExpandableTabBar.ExpandableTabBarItem>()
            { moneytabBarItem, storagetabBarItem, userstabBarItem, statistictabBarItem, storagetabBarItem1, userstabBarItem1});

            //var tabBar = TabBar;
            //TabBar.BackgroundColor = UIColor.LightGray;
            //TabBar.TintColor = UIColor.White;

                //UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes() { TextColor = UIColor.White }, UIControlState.Normal);
                     //UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes() {  TextColor = UIColor.Black }, UIControlState.Selected);
        
        }

    }
}

