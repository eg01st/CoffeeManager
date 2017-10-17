
using Foundation;
using UIKit;
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
            tabBar.UnselectedItemTintColor = UIColor.Black;
            tabBar.SelectedImageTintColor = UIColor.FromRGB(0, 122, 255);
        }

    }
}

