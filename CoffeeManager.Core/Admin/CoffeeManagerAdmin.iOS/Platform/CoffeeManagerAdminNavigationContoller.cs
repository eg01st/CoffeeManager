using System;
using System.Linq;
using Foundation;
using UIKit;
using CoffeManager.Common;
using MvvmCross.iOS.Views;

namespace CoffeeManagerAdmin.iOS
{
    public class CoffeeManagerAdminNavigationContoller: MvxNavigationController, IUINavigationControllerDelegate, IUIGestureRecognizerDelegate
    {
        public CoffeeManagerAdminNavigationContoller(UIViewController viewController) : base(viewController)
        {
            Delegate = this;
            InteractivePopGestureRecognizer.Delegate = this;
            InteractivePopGestureRecognizer.Enabled = true;
        }

        [Export("navigationController:willShowViewController:animated:")]
        public void WillShowViewController(UINavigationController navigationController, UIViewController viewController, bool animated)
        {
            
        }

        [Export("gestureRecognizer:shouldBeRequiredToFailByGestureRecognizer:")]
        public bool ShouldBeRequiredToFailBy(UIGestureRecognizer gestureRecognizer, UIGestureRecognizer otherGestureRecognizer)
        {
            return true;
        }

        [Export("navigationController:didShowViewController:animated:")]
        public void DidShowViewController(UINavigationController navigationController, UIViewController viewController, bool animated)
        {
            bool isRootController = viewController == navigationController.ViewControllers.FirstOrDefault();
            navigationController.InteractivePopGestureRecognizer.Enabled = !isRootController;
        }
    }
}
