using System;
using CoreGraphics;
using Foundation;
using MvvmCross.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public abstract class ViewControllerBase : MvxViewController
    {
        private NSLayoutConstraint buttonBottomConstraint;
        private nfloat buttonBottomMargin;
        private const double layoutKeyboardAnimationSpeed = 0.35;
        private NSObject keyboardShowObserver;
        private NSObject keyboardHideObserver;

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

        public override void ViewDidLoad()
        {
  
            base.ViewDidLoad();
            InitStylesAndContent();
            DoBind();
        }

        protected virtual void DoBind()
        {
        
        }

        protected virtual void InitStylesAndContent()
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
    }
}
