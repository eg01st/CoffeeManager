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
using MvvmCross.iOS.Views.Presenters.Attributes;

namespace CoffeeManagerAdmin.iOS
{
    [Register("MainView")]
    [MvxRootPresentation()]
    public partial class MainView : MainTabController
    {
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
            var tabBar = TabBar;
           // TabBar.BackgroundColor = UIColor.LightGray;
          //  TabBar.TintColor = UIColor.White;

          //  UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes() { TextColor = UIColor.White }, UIControlState.Normal);
           // UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes() {  TextColor = UIColor.Black }, UIControlState.Selected);
        
        }

    }
}

