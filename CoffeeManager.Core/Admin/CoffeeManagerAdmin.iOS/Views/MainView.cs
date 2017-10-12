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
            var view = new ExpandableTabBar(this);
            view.BackgroundColor = UIColor.GroupTableViewBackgroundColor;

            var button = new UIButton(new CGRect(0, 0, 50, 50));
            button.SetTitle("1", UIControlState.Normal);
            button.TouchUpInside += (sender, e) => SelectedIndex = 0;
            button.BackgroundColor = UIColor.Orange;
            view.AddTabItem(button);

            var button1 = new UIButton(new CGRect(50, 0, 50, 50));
            button1.SetTitle("2", UIControlState.Normal);
            button1.TouchUpInside += (sender, e) => SelectedIndex = 1;
            button1.BackgroundColor = UIColor.Magenta;
            view.AddTabItem(button1);

            var button2 = new UIButton(new CGRect(100, 0, 50, 50));
            button2.SetTitle("3", UIControlState.Normal);
            button2.TouchUpInside += (sender, e) => SelectedIndex = 2;
            button2.BackgroundColor = UIColor.Green;
            view.AddTabItem(button2);


            var button3 = new UIButton(new CGRect(150, 0 , 50, 50));
            button3.SetTitle("4", UIControlState.Normal);
            button3.BackgroundColor = UIColor.Yellow;
            button3.TouchUpInside += (sender, e) => SelectedIndex = 3;
            view.AddTabItem(button3);
          

                //var tabBar = TabBar;
                //TabBar.BackgroundColor = UIColor.LightGray;
                //TabBar.TintColor = UIColor.White;

            //    UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes() { TextColor = UIColor.White }, UIControlState.Normal);
       //         UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes() {  TextColor = UIColor.Black }, UIControlState.Selected);
        }

    }
}

