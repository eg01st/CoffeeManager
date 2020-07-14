using System.Linq;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.iOS.Views.Presenters.Attributes;
using UIKit;

namespace MobileCore.iOS.Presenters
{
    public class CustomIosViewPresenter : MvxIosViewPresenter
    {
        public ViewPagerController PageViewController { get; private set; }

        public CustomIosViewPresenter(IUIApplicationDelegate applicationDelegate, UIWindow window) : base(applicationDelegate, window)
        {
        }

        public override void RegisterAttributeTypes()
        {
            base.RegisterAttributeTypes();

            var pageChildViewAction = new MvxPresentationAttributeAction
            {
                ShowAction = (viewType, attribute, request) =>
                {
                    var viewController = (UIViewController)this.CreateViewControllerFor(request);
                    ShowPageChildViewController(viewController, (PageChildViewPresentationAttribute)attribute, request);
                },
                CloseAction = ClosePageViewChild
            };

            AttributeTypesToActionsDictionary.TryGetValue(typeof(MvxChildPresentationAttribute), out var childAction);
            AttributeTypesToActionsDictionary.TryAdd(typeof(PageRootViewPresentationAttribute), childAction);
            AttributeTypesToActionsDictionary.TryAdd(typeof(PageChildViewPresentationAttribute), pageChildViewAction);
        }

		protected override void ShowChildViewController(UIViewController viewController, MvxChildPresentationAttribute attribute, MvxViewModelRequest request)
		{
            var pageViewControllerAttribute = attribute as PageRootViewPresentationAttribute;
            if (pageViewControllerAttribute != null)
            {
                PageViewController = viewController as ViewPagerController;
            }

            base.ShowChildViewController(viewController, attribute, request);
		}

		private bool ClosePageViewChild(IMvxViewModel viewModel, MvxBasePresentationAttribute attribute)
        {
            var viewControllers = PageViewController.ContentViewControllers;
            var viewControllerToClose = viewControllers.FirstOrDefault(vc => vc.GetIMvxIosView().ViewModel == viewModel);
            if (viewControllerToClose != null)
            {
                viewControllers.Remove(viewControllerToClose);
                PageViewController.GetPreviousViewController(PageViewController, viewControllerToClose);
                return true;
            }

            return false;
        }

		private void ShowPageChildViewController(UIViewController viewController, PageChildViewPresentationAttribute attribute, MvxViewModelRequest request)
        {
            if (PageViewController != null)
            {
                var viewControllers = PageViewController.ContentViewControllers.ToList();
                if (!viewControllers.Any())
                {
                    PageViewController.ContentViewControllers.Add(viewController);
                    PageViewController.SetViewControllers(new[] { viewController }, UIPageViewControllerNavigationDirection.Forward, true, null);
                }
                else
                {
                    PageViewController.ContentViewControllers.Add(viewController);
                }
            }
        }
    }
}