using MvvmCross.iOS.Views;
using UIKit;
using MvvmCross.iOS.Views.Presenters;

namespace CoffeeManagerAdmin.iOS.Platform
{
    public class CoffeeManagerAdminPresenter : MvxIosViewPresenter
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
