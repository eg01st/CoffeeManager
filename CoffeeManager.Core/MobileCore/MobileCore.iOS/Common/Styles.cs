using System;
using UIKit;

namespace MobileCore.iOS
{
    public static class Styles
    {
        public static void ApplyNavigationControllerStyle(UINavigationController navigationController, UINavigationItem navigationItem, UINavigationItemLargeTitleDisplayMode largeTitleDisplayMode)
        {
            if (navigationController != null && DeviceHelper.IsIos11AndGreater)
            {
                var navigationBar = navigationController.NavigationBar;
                navigationItem.LargeTitleDisplayMode = largeTitleDisplayMode;
                navigationItem.HidesSearchBarWhenScrolling = true;
                navigationBar.PrefersLargeTitles = false;
            }
        }
    }
}