using System;
using System.Linq;
using Foundation;
using UIKit;
using CoffeManager.Common;
using MvvmCross.iOS.Views;

namespace CoffeeManagerAdmin.iOS
{
    public class CoffeeManagerAdminNavigationContoller: UINavigationController, IUINavigationControllerDelegate, IUIGestureRecognizerDelegate
    {
        public CoffeeManagerAdminNavigationContoller(UIViewController viewController) : base(viewController)
        {
            Delegate = this;
            InteractivePopGestureRecognizer.Delegate = this;
            InteractivePopGestureRecognizer.Enabled = true;
        }


        public override UIViewController PopViewController(bool animated)
        {
            var vc =  base.PopViewController(animated);
            var mvxVc = vc as MvxViewController;
            var vm = mvxVc.ViewModel as ViewModelBase;
            vm.CloseCommand.Execute(null);
            return vc;
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
