﻿using System;
using CoreGraphics;
using Foundation;
using MobileCore.ViewModels;
using UIKit;

namespace MobileCore.iOS.ViewControllers
{
    public abstract class KeyboardViewControllerBase<TViewModel> : ViewControllerBase<TViewModel>
        where TViewModel : PageViewModel
    {
        private NSObject keyBoardWillShow;
        private NSObject keyBoardWillHide;
        private UITextField[] formTextfields;

        protected KeyboardViewControllerBase()
            : base()
        {
        }

        protected KeyboardViewControllerBase(string nibName)
            : base(nibName, null)
        {
        }

        protected KeyboardViewControllerBase(string nibName, NSBundle bundle)
            : base(nibName, bundle)
        {
        }

        protected KeyboardViewControllerBase(IntPtr ptr)
            : base(ptr)
        {
        }

        protected virtual Type KeyboardElementType => typeof(UIScrollView);

        protected virtual int VerticalSpacingFromKeyboard { get; set; } = 10;

        protected virtual bool HandlesKeyboardNotifications => false;

        protected UIView ViewToCenterOnKeyboardShown { get; set; }

        /// <summary>
        /// Gets the UIView that represents the "active" user input control (e.g. textfield, or button under a text field)
        /// </summary>
        /// <returns>
        /// A <see cref="UIView"/>
        /// </returns>
        protected virtual UIView KeyboardGetActiveView() => View.GetFirstResponder();

        protected override void DoViewWillAppear(bool animated)
        {
            base.DoViewWillAppear(animated);

            if (HandlesKeyboardNotifications)
            {
                RegisterForKeyboardNotifications();
            }
        }

        protected override void DoViewWillDisappear(bool animated)
        {
            base.DoViewWillDisappear(animated);

            if (HandlesKeyboardNotifications)
            {
                UnregisterForKeyboardNotifications();
            }
        }

        protected virtual void RegisterForKeyboardNotifications()
        {
            keyBoardWillHide = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
            keyBoardWillShow = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
        }

        protected virtual void UnregisterForKeyboardNotifications()
        {
            if (!IsViewLoaded)
            {
                return;
            }

            NSNotificationCenter.DefaultCenter.RemoveObserver(keyBoardWillShow);
            keyBoardWillShow.Dispose();
            keyBoardWillShow = null;

            NSNotificationCenter.DefaultCenter.RemoveObserver(keyBoardWillHide);
            keyBoardWillHide.Dispose();
            keyBoardWillHide = null;
        }

        private void OnKeyboardNotification(NSNotification notification)
        {
            if (!IsViewLoaded)
            {
                return;
            }

            // Check if the keyboard is becoming visible
            var visible = notification.Name == UIKeyboard.WillShowNotification;

            // Start an animation, using values from the keyboard
            UIView.BeginAnimations("AnimateForKeyboard");
            UIView.SetAnimationBeginsFromCurrentState(true);
            UIView.SetAnimationDuration(UIKeyboard.AnimationDurationFromNotification(notification));
            UIView.SetAnimationCurve((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification(notification));

            // Pass the notification, calculating keyboard height, etc.
            var keyboardFrame = visible
                ? UIKeyboard.FrameEndFromNotification(notification)
                : UIKeyboard.FrameBeginFromNotification(notification);

            OnKeyboardChanged(visible, keyboardFrame.Height);

            // Commit the animation
            UIView.CommitAnimations();
        }

        /// <summary>
        /// Override this method to apply custom logic when the keyboard is shown/hidden
        /// </summary>
        /// <param name='visible'>
        /// If the keyboard is visible
        /// </param>
        /// <param name='keyboardHeight'>
        /// Calculated height of the keyboard (width not generally needed here)
        /// </param>
        protected virtual void OnKeyboardChanged(bool visible, nfloat keyboardHeight)
        {
            var activeView = ViewToCenterOnKeyboardShown = KeyboardGetActiveView();
            if (activeView == null)
            {
                return;
            }

            var scrollView = activeView.FindSuperviewOfType(View, KeyboardElementType) as UIScrollView;
            if (scrollView == null)
            {
                return;
            }

            if (!visible)
            {
                RestoreScrollPosition(scrollView);
            }
            else
            {
                CenterViewInScroll(activeView, scrollView, keyboardHeight);
            }
        }

        protected virtual void CenterViewInScroll(UIView viewToCenter, UIScrollView scrollView, nfloat keyboardHeight)
        {
            var contentInsets = new UIEdgeInsets(0.0f, 0.0f, keyboardHeight, 0.0f);
            scrollView.ContentInset = contentInsets;
            scrollView.ScrollIndicatorInsets = contentInsets;

            // Position of the active field relative isnside the scroll view
            // If activeField is hidden by keyboard, scroll it so it's visible
            CGRect viewRectAboveKeyboard = new CGRect(this.View.Frame.Location, new CGSize(this.View.Frame.Width, this.View.Frame.Size.Height - keyboardHeight));

            CGRect activeFieldAbsoluteFrame = ViewToCenterOnKeyboardShown.Superview.ConvertRectToView(ViewToCenterOnKeyboardShown.Frame, this.View);

            // activeFieldAbsoluteFrame is relative to this.View so does not include any scrollView.ContentOffset
            // fix y position
            if (activeFieldAbsoluteFrame.Y < viewRectAboveKeyboard.Y)
            {
                activeFieldAbsoluteFrame.Y += viewRectAboveKeyboard.Y;
            }
            else
            {
                activeFieldAbsoluteFrame.Y += 5f;
            }

            // Check if the activeField will be partially or entirely covered by the keyboard
            if (!viewRectAboveKeyboard.Contains(activeFieldAbsoluteFrame))
            {
                ScrollToPointKeyboardAction(scrollView, activeFieldAbsoluteFrame, viewRectAboveKeyboard, keyboardHeight);
            }
        }

        protected virtual void ScrollToPointKeyboardAction(UIScrollView scrollView, CGRect activeFieldAbsoluteFrame, CGRect viewRectAboveKeyboard, nfloat keyboardHeight)
        {
            // Scroll to the activeField Y position + activeField.Height + current scrollView.ContentOffset.Y - the keyboard Height
            CGPoint scrollPoint = new CGPoint(0.0f, activeFieldAbsoluteFrame.Location.Y + activeFieldAbsoluteFrame.Height + scrollView.ContentOffset.Y - viewRectAboveKeyboard.Height + VerticalSpacingFromKeyboard);
            scrollView.SetContentOffset(scrollPoint, true);
        }

        protected virtual void RestoreScrollPosition(UIScrollView scrollView)
        {
            scrollView.ContentInset = UIEdgeInsets.Zero;
            scrollView.ScrollIndicatorInsets = UIEdgeInsets.Zero;
        }

        /// <summary>
        /// Call it to force dismiss keyboard when background is tapped
        /// </summary>
        protected virtual void DismissKeyboardOnBackgroundTap(bool cancelsTouchesInView = false)
        {
            // Add gesture recognizer to hide keyboard
            var tap = new UITapGestureRecognizer { CancelsTouchesInView = cancelsTouchesInView };
            tap.AddTarget(() => View.EndEditing(true));
            View.AddGestureRecognizer(tap);
        }

        public void EnableNextKeyForTextFields(params UITextField[] fields)
        {
            formTextfields = fields;

            foreach (var field in fields)
            {
                field.ShouldReturn += ShouldReturn;
            }
        }

        private bool ShouldReturn(UITextField textField)
        {
            var index = Array.IndexOf(formTextfields, textField);

            if (index > -1 && index < formTextfields.Length - 1)
            {
                formTextfields[index + 1].BecomeFirstResponder();

                return true;
            }
            else if (index == formTextfields.Length - 1)
            {
                formTextfields[index].ResignFirstResponder();

                FormFinished();
            }

            return false;
        }

        protected virtual void FormFinished()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && formTextfields != null)
            {
                foreach (var item in formTextfields)
                {
                    item.ShouldReturn -= ShouldReturn;
                }

                formTextfields = null;
            }

            base.Dispose(disposing);
        }
    }
}
