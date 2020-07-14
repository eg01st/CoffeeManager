using System;
using UIKit;

namespace MobileCore.iOS
{
    public static class UIViewExtensions
    {
        public static UIView GetFirstResponder(this UIView superView)
        {
            if (superView.IsFirstResponder)
            {
                return superView;
            }

            foreach (var subView in superView.Subviews)
            {
                var firstResponder = subView.GetFirstResponder();
                if (firstResponder != null)
                {
                    return firstResponder;
                }
            }

            return null;
        }

        public static UIView FindSuperviewOfType(this UIView view, UIView stopAt, Type type)
        {
            if (view.Superview != null)
            {
                if (type.IsInstanceOfType(view.Superview))
                {
                    return view.Superview;
                }

                if (view.Superview != stopAt)
                {
                    return view.Superview.FindSuperviewOfType(stopAt, type);
                }
            }

            return null;
        }
    }
}
