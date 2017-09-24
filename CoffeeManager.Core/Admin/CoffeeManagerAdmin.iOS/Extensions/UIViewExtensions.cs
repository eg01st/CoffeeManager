using System;
using System.Linq;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public static class ConstraintExtensions
    {
        public static NSLayoutConstraint TopToTop(UIView firstItem,
                                                            UIView secondItem,
                                                            nfloat constant,
                                                            float multiplier = 1.0f,
                                                            NSLayoutRelation relation = NSLayoutRelation.Equal)
        {
            return NSLayoutConstraint.Create(firstItem,
                                             NSLayoutAttribute.Top,
                                             relation,
                                             secondItem,
                                             NSLayoutAttribute.Top,
                                             multiplier,
                                             constant);
        }



        public static NSLayoutConstraint LeadingToLeading(UIView firstItem,
                                                                    UIView secondItem,
                                                                    nfloat constant,
                                                                    float multiplier = 1.0f,
                                                                    NSLayoutRelation relation = NSLayoutRelation.Equal)
        {
            return NSLayoutConstraint.Create(firstItem,
                                             NSLayoutAttribute.Leading,
                                             relation,
                                             secondItem,
                                             NSLayoutAttribute.Leading,
                                             multiplier,
                                             constant);
        }

        public static NSLayoutConstraint TrailingToTrailing(UIView firstItem,
                                                                      UIView secondItem,
                                                                      nfloat constant,
                                                                      float multiplier = 1.0f,
                                                                      NSLayoutRelation relation = NSLayoutRelation.Equal)
        {
            return NSLayoutConstraint.Create(firstItem,
                                             NSLayoutAttribute.Trailing,
                                             relation,
                                             secondItem,
                                             NSLayoutAttribute.Trailing,
                                             multiplier,
                                             constant);
        }

        public static NSLayoutConstraint BottomToBottom(UIView firstItem,
                                                                  UIView secondItem,
                                                                  nfloat constant,
                                                                  float multiplier = 1.0f,
                                                                  NSLayoutRelation relation = NSLayoutRelation.Equal)
        {
            return NSLayoutConstraint.Create(firstItem,
                                             NSLayoutAttribute.Bottom,
                                             relation,
                                             secondItem,
                                             NSLayoutAttribute.Bottom,
                                             multiplier,
                                             constant);
        }

        public static NSLayoutConstraint CenterVertically(UIView firstItem,
                                                                    UIView secondItem,
                                                                    float constant = 0f,
                                                                    float multiplier = 1f,
                                                                    NSLayoutRelation relation = NSLayoutRelation.Equal)
        {
            return NSLayoutConstraint.Create(firstItem,
                                             NSLayoutAttribute.CenterY,
                                             NSLayoutRelation.Equal,
                                             secondItem,
                                             NSLayoutAttribute.CenterY,
                                             multiplier,
                                             constant);
        }

        public static NSLayoutConstraint CenterHorizontally(UIView firstItem,
                                                                      UIView secondItem,
                                                                      float constant = 0f,
                                                                      float multiplier = 1f,
                                                                      NSLayoutRelation relation = NSLayoutRelation.Equal)
        {
            return NSLayoutConstraint.Create(firstItem,
                                             NSLayoutAttribute.CenterX,
                                             NSLayoutRelation.Equal,
                                             secondItem,
                                             NSLayoutAttribute.CenterX,
                                             multiplier,
                                             constant);
        }

        public static NSLayoutConstraint Width(UIView view,
                                                         float constant,
                                                         float multiplier = 1.0f,
                                                         NSLayoutRelation relation = NSLayoutRelation.Equal)
        {
            return EqualWidths(view, null, constant, multiplier, relation);
        }

        public static NSLayoutConstraint Height(UIView view,
                                                          float constant,
                                                          float multiplier = 1.0f,
                                                          NSLayoutRelation relation = NSLayoutRelation.Equal)
        {
            return EqualHeights(view, null, constant, multiplier, relation);
        }

        public static NSLayoutConstraint EqualWidths(UIView firstItem,
                                                               UIView secondItem,
                                                               float constant = 0f,
                                                               float multiplier = 1f,
                                                               NSLayoutRelation relation = NSLayoutRelation.Equal)
        {
            return NSLayoutConstraint.Create(firstItem,
                                             NSLayoutAttribute.Width,
                                             relation,
                                             secondItem,
                                             NSLayoutAttribute.Width,
                                             multiplier,
                                             constant);
        }

        public static NSLayoutConstraint EqualHeights(UIView firstItem,
                                                                UIView secondItem,
                                                                float constant = 0f,
                                                                float multiplier = 1.0f,
                                                                NSLayoutRelation relation = NSLayoutRelation.Equal)
        {
            return NSLayoutConstraint.Create(firstItem,
                                             NSLayoutAttribute.Height,
                                             relation,
                                             secondItem,
                                             NSLayoutAttribute.Height,
                                             multiplier,
                                             constant);
        }

        public static NSLayoutConstraint HorizontalSpacing(UIView rightView,
                                                                     UIView leftView,
                                                                     float spacing,
                                                                     float multiplier = 1.0f,
                                                                     NSLayoutRelation relation = NSLayoutRelation.Equal)
        {
            return NSLayoutConstraint.Create(rightView,
                                             NSLayoutAttribute.Left,
                                             relation,
                                             leftView,
                                             NSLayoutAttribute.Right,
                                             multiplier,
                                             spacing);
        }

        public static NSLayoutConstraint VerticalSpacing(UIView bottomView,
                                                                   UIView topView,
                                                                   float spacing,
                                                                   float multiplier = 1.0f,
                                                                   NSLayoutRelation relation = NSLayoutRelation.Equal)
        {
            return NSLayoutConstraint.Create(bottomView,
                                             NSLayoutAttribute.Top,
                                             relation,
                                             topView,
                                             NSLayoutAttribute.Bottom,
                                             multiplier,
                                             spacing);
        }


        public static NSLayoutConstraint[] StickToSuperViewEdges(UIView superView,
                                                                       UIView subView,
                                                                       UIRectEdge[] edgesForConstraints,
                                                                       UIEdgeInsets distancesFromEdges)
        {
            return edgesForConstraints
                .Select(edge => ConstraintForEdge(superView, subView, edge, distancesFromEdges))
                .ToArray();
        }

        public static NSLayoutConstraint[] StickToAllSuperViewEdges(UIView superView, UIView subView)
        {
            return StickToSuperViewEdges(
                superView,
                subView,
                new[]
                {
                    UIRectEdge.Top,
                    UIRectEdge.Left,
                    UIRectEdge.Right,
                    UIRectEdge.Bottom
                },
                UIEdgeInsets.Zero);
        }

        private static NSLayoutConstraint ConstraintForEdge(UIView superView,
                                                            UIView subView,
                                                            UIRectEdge edge,
                                                            UIEdgeInsets distancesFromEdges)
        {
            switch (edge)
            {
                case UIRectEdge.Top:
                    return TopToTop(subView, superView, distancesFromEdges.Top);
                case UIRectEdge.Left:
                    return LeadingToLeading(subView, superView, distancesFromEdges.Left);
                case UIRectEdge.Right:
                    return TrailingToTrailing(superView, subView, distancesFromEdges.Right);
                case UIRectEdge.Bottom:
                    return BottomToBottom(superView, subView, distancesFromEdges.Bottom);
                default:
                    throw new Exception();
            }
        }
    }
}
