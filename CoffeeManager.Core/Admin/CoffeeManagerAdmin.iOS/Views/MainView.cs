using System;
using CoffeeManagerAdmin.Core.ViewModels;
using MobileCore.iOS.Common;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views
{
    [MvxRootPresentation]
    public partial class MainView : MvxTabBarViewController<MainViewModel>
    {
        private bool isPresentedFirstTime = true;

        public MainView(IntPtr handle) : base(handle)
        {
        }

        public MainView() : base()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;
            TabBar.Translucent = false;
            TabBar.BackgroundColor = Colors.Black;
            TabBar.BarStyle = UIBarStyle.Black;
            TabBar.TintColor = UIColor.White;
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (ViewModel != null && isPresentedFirstTime)
            {
                isPresentedFirstTime = false;
                await ViewModel.ShowInitialViewModelsAsync();
            }
        }

        //public async override void ViewWillAppear(bool animated)
        //{
        //    base.ViewWillAppear(animated);
          
        //    NavigationItem.SetHidesBackButton(true, false);
        //}

        //private void InitTabBar()
        //{
        //    var tabBar = TabBar;
        //    tabBar.UnselectedItemTintColor = UIColor.Black;
        //    tabBar.SelectedImageTintColor = UIColor.FromRGB(0, 122, 255);
        //}

    }
}

