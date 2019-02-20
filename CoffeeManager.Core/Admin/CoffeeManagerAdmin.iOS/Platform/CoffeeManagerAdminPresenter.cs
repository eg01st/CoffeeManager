using MobileCore.iOS.Presenters;
using MvvmCross.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Platform
{
    public class CoffeeManagerAdminPresenter : CustomIosViewPresenter
    {
        public CoffeeManagerAdminPresenter(IUIApplicationDelegate applicationDelegate, UIWindow window) : base(applicationDelegate, window)
        {
        }

        protected override MvxNavigationController CreateNavigationController(UIViewController viewController)
        {
            return new CoffeeManagerAdminNavigationContoller(viewController);
        }
    }
}
