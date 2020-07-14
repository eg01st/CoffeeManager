using System;
using CoreGraphics;
using Foundation;
using MobileCore.ViewModels;
using MvvmCross.iOS.Views;
using UIKit;

namespace MobileCore.iOS.ViewControllers
{
    public abstract class ViewControllerBase<TViewModel> :  MvxViewController<TViewModel>
        where TViewModel : PageViewModel
    {
        private bool isFirstLoad = true;

        private NSLayoutConstraint buttonBottomConstraint;
        private nfloat buttonBottomMargin;
        private const double layoutKeyboardAnimationSpeed = 0.35;
        private NSObject keyboardShowObserver;
        private NSObject keyboardHideObserver;

        protected virtual bool UseCustomBackButton { get; set; } = true;

        protected virtual bool HideNavBar => false;


       protected ViewControllerBase()
            :base()
        {
        }

        protected ViewControllerBase(string nibName)
            : base(nibName, null)
        {
        }

        protected ViewControllerBase(string nibName, NSBundle bundle)
            : base(nibName, bundle)
        {
        }

        protected ViewControllerBase(IntPtr ptr)
            : base(ptr)
        {
        }


        protected virtual void InitNavigationItem(UINavigationItem navigationItem)
        {
            if (UseCustomBackButton)
            {
                var backButton = CreateImageBarButton("ic_arrow_back.png", OnBackButtonPressed);
                navigationItem.SetLeftBarButtonItem(backButton, false);
            }
        }

        protected virtual void OnBackButtonPressed(object sender, EventArgs e)
        {
            CloseViewModel();
        }

        protected void CloseViewModel()
        {
            ViewModel.CloseCommand.Execute(null);
        }

        protected virtual void InitNavigationController(UINavigationController navigationController)
        {

        }

        private void InitNavigationController()
        {
            var navigationController = NavigationController;
            if (navigationController == null)
                return;
            
            NavigationController.NavigationBar.Translucent = false;
            navigationController.SetNavigationBarHidden(HideNavBar, false);
            InitNavigationController(navigationController);
        }

        public override void ViewDidLoad()
        {
  
            base.ViewDidLoad();

            DoViewDidLoad();
            InitStylesAndContent();

            InitNavigationItem(NavigationItem);
            InitNavigationController();


            DoBind();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            DoViewWillAppear(animated);
            UnsubscribeFromEvents();
            SubscribeToEvents();

            if (isFirstLoad && ViewModel != null)
            {
                isFirstLoad = false;
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            DoViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            UnsubscribeFromEvents();
            DoViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            DoViewDidDisappear(animated);
        }


        protected virtual void DoViewDidLoad()
        {
            
        }

        protected virtual void DoBind()
        {
        
        }

        protected virtual void InitStylesAndContent()
        {
        }

        protected virtual void DoViewWillAppear(bool animated)
        {
        }

        protected virtual void DoViewDidAppear(bool animated)
        {
        }

        protected virtual void DoViewWillDisappear(bool animated)
        {
        }

        protected virtual void DoViewDidDisappear(bool animated)
        {
        }

        protected virtual void SubscribeToEvents()
        {
        }

        protected virtual void UnsubscribeFromEvents()
        {
        }

        protected virtual void DoViewWillLayoutSubviews()
        {
        }

        protected virtual void DoViewDidLayoutSubviews()
        {
        }
     

        #region StickButton

        protected void StickBottomButtonToKeyboard(NSLayoutConstraint buttonBottomConstraint)
        {
            this.buttonBottomConstraint = buttonBottomConstraint;
            buttonBottomMargin = buttonBottomConstraint.Constant;
            SubscribeTokeyboardNotifications();
        }

        protected void SubscribeTokeyboardNotifications()
        {
            var defaultCenter = NSNotificationCenter.DefaultCenter;
            keyboardShowObserver = defaultCenter.AddObserver(UIKeyboard.WillShowNotification, KeyboardWillShow);
            keyboardHideObserver = defaultCenter.AddObserver(UIKeyboard.DidHideNotification, KeyboardDidHide);
        }

        protected void UnSubscribeKeyboardEvents()
        {
            var defaultCenter = NSNotificationCenter.DefaultCenter;
            if (keyboardShowObserver != null)
            {
                defaultCenter.RemoveObserver(keyboardShowObserver);
            }
            if (keyboardHideObserver != null)
            {
                defaultCenter.RemoveObserver(keyboardHideObserver);
            }
        }

        protected virtual void KeyboardWillShow(NSNotification notification)
        {
            CGSize keyboardSize = UIKeyboard.FrameEndFromNotification(notification).Size;
            var keyboardHeight = keyboardSize.Height;
            buttonBottomConstraint.Constant = keyboardHeight;
            AnimateWithLayout();
        }

        protected virtual void KeyboardDidHide(NSNotification notification)
        {
            buttonBottomConstraint.Constant = buttonBottomMargin;
            AnimateWithLayout();
        }

        private void AnimateWithLayout()
        {
            UIView.Animate(layoutKeyboardAnimationSpeed, () =>
            {
                View.LayoutIfNeeded();
            });
        }

        #endregion

        private UIBarButtonItem CreateImageBarButton(string imageKey, EventHandler handler)
        {
            var image = new UIImage(imageKey);
            var button = UIButton.FromType(UIButtonType.Custom);
            button.SetImage(image, UIControlState.Normal);
            button.BackgroundColor = UIColor.Clear;
            var imageSize = image.Size;
            button.Frame = new CGRect(0.0f, 0.0f, imageSize.Width, imageSize.Height);

            button.TouchUpInside += handler;
            var barButton = new UIBarButtonItem(button);

            return barButton;
        }

    }
}
