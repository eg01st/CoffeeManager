using System;
using CoreGraphics;
using UIKit;
namespace CoffeeManagerAdmin.iOS
{
    public class ExpandableTabBar : UIView
    {
        private readonly UIImage expandImage;
        private readonly UIImage collapseImage;
        private UIButton expandButton;

        private bool isTabBarExpanded = false;

        public ExpandableTabBar(UITabBarController tabController)
        {
            expandImage = UIImage.FromBundle("ic_expand.png");
            collapseImage = UIImage.FromBundle("ic_colapse.png");

            TranslatesAutoresizingMaskIntoConstraints = false;
            StickToTabController(tabController.View);
            InitSwipeRecognizers();
        }

        public void AddTabItem(UIButton button)
        {
            this.AddSubview(button);
        }

        private void InitSwipeRecognizers()
        {
            var swipeUpRecognizer = new UISwipeGestureRecognizer((UISwipeGestureRecognizer obj) =>
            {
                ExpandTabBar();
            })
            { Direction = UISwipeGestureRecognizerDirection.Up };

            var swipeDownRecognizer = new UISwipeGestureRecognizer((UISwipeGestureRecognizer obj) =>
            {
                CollapseTabBar();
            })
            { Direction = UISwipeGestureRecognizerDirection.Down };

            this.AddGestureRecognizer(swipeUpRecognizer);
            this.AddGestureRecognizer(swipeDownRecognizer);
        }

        private void StickToTabController(UIView tabControllerView)
        {
            tabControllerView.AddSubview(this);

            var cons = new[]
            {
                ConstraintExtensions.EqualWidths(tabControllerView, this),
                ConstraintExtensions.LeadingToLeading(tabControllerView, this, 0),
                ConstraintExtensions.TrailingToTrailing(this, tabControllerView, 0),
                ConstraintExtensions.BottomToBottom(this, tabControllerView, 50),
            };

            this.AddConstraint(ConstraintExtensions.Height(this, 100));

            expandButton = new UIButton(UIButtonType.Custom);
            expandButton.TranslatesAutoresizingMaskIntoConstraints = false;
            expandButton.SetImage(expandImage, UIControlState.Normal);
            expandButton.TouchUpInside += ExpandButton_TouchUpInside;

            this.AddSubview(expandButton);

            expandButton.AddConstraints(new []{ 
                ConstraintExtensions.Height(expandButton, 50),
                ConstraintExtensions.Width(expandButton, 50)
            });

            this.AddConstraints(new []{
                ConstraintExtensions.TopToTop(this, expandButton, 0),
                ConstraintExtensions.TrailingToTrailing(expandButton, this, 0)
            });

            tabControllerView.AddConstraints(cons);
        }

        private void CollapseTabBar()
        {
            if (!isTabBarExpanded)
            {
                return;
            }
            isTabBarExpanded = false;
            expandButton.SetImage(expandImage, UIControlState.Normal);
            UIView.Animate(0.3, () =>
            {
                var bounds = this.Bounds;
                this.Center = new CGPoint(this.Center.X, this.Center.Y + bounds.Height / 2);
            });
        }

        private void ExpandTabBar()
        {
            if (isTabBarExpanded)
            {
                return;
            }
            isTabBarExpanded = true;
            expandButton.SetImage(collapseImage, UIControlState.Normal);
            UIView.Animate(0.3, () =>
            {
                var bounds = this.Bounds;
                this.Center = new CGPoint(this.Center.X, this.Center.Y - bounds.Height / 2);
            });
        }

        private void ExpandButton_TouchUpInside(object sender, EventArgs e)
        {
            if(isTabBarExpanded)
            {
                CollapseTabBar();
            }
            else
            {
                ExpandTabBar();
            }
        }
    }
}
