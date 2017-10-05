using System;
using CoffeManager.Common;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public class CoffeeManagerAdminPresenter : MvxIosViewPresenter
    {
        public CoffeeManagerAdminPresenter(IUIApplicationDelegate applicationDelegate, UIWindow window) : base(applicationDelegate, window)
        {
        }

        protected override UINavigationController CreateNavigationController(UIViewController viewController)
        {
            return new CoffeeManagerAdminNavigationContoller(viewController);
        }

        public override void Show(MvxViewModelRequest request)
        {
            var requestedController = this.CreateViewControllerFor(request);
            if (IsViewControllerPresented(requestedController))
            {
                return;
            }

            if (request.IsRootRequest())
            {
                ShowAsRoot(requestedController);
            }
            else
            {
                base.Show(request);
            }
        }

        private bool IsViewControllerPresented(IMvxIosView requestedController)
        {
            var requestedVcType = requestedController.GetType();
            if (requestedVcType == typeof(MainView))
            {
                return false;
            }

            var presentController = MasterNavigationController?.TopViewController;
            return requestedVcType == presentController?.GetType();
        }

        private void ShowAsRoot(IMvxIosView view)
        {
            var viewController = CastToViewController(view);
            MasterNavigationController.SetViewControllers(new UIViewController[] { viewController }, true);
        }

        private UIViewController CastToViewController(IMvxIosView view)
        {
            var viewController = view as UIViewController;
            if (viewController == null)
            {
                throw new MvvmCross.Platform.Exceptions.MvxException("Passed in IMvxIosView is not a UIViewController");
            }
            return viewController;
        }
    }
}
