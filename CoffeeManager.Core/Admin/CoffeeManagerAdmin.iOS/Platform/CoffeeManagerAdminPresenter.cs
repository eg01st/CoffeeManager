using System;
using CoffeManager.Common;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters;
using UIKit;
using CoffeeManagerAdmin.Core.ViewModels;
using MvvmCross.iOS.Views.Presenters.Attributes;

namespace CoffeeManagerAdmin.iOS
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
