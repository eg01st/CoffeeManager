using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;
namespace CoffeeManagerAdmin.iOS
{
    public class ExpandableTabBar : UIView
    {
        public class ExpandableTabBarItem
        {
            public ExpandableTabBarItem(int tabIndex, string imagePath, string selectedImagePath, string text)
            {
                TabbarIndex = tabIndex;
                ImagePath = imagePath;
                SelectedImagePath = selectedImagePath;
                Text = text;
            }

            public int TabbarIndex { get; set; }
            public string ImagePath { get; set; }
            public string SelectedImagePath { get; set; }
            public string Text { get; set; }
        }


        private readonly UIImage expandImage;
        private readonly UIImage collapseImage;
        private UIButton expandButton;
        private UITabBarController tabController;


        private bool isTabBarExpanded = false;

        public ExpandableTabBar(UITabBarController tabController)
        {
            this.tabController = tabController;
            tabController.View.WillRemoveSubview(tabController.TabBar);
            expandImage = UIImage.FromBundle("ic_expand.png");
            collapseImage = UIImage.FromBundle("ic_colapse.png");

            TranslatesAutoresizingMaskIntoConstraints = false;
            BackgroundColor = UIColor.GroupTableViewBackgroundColor;
            StickToTabController(tabController.View);
            InitSwipeRecognizers();
        }

        public void AddTabItems(List<ExpandableTabBarItem> items)
        {
            int itemsInRow = 0;
            UIView previousView = new UIView();
            var colors = new[] { UIColor.Blue, UIColor.Brown, UIColor.Cyan, UIColor.DarkGray, UIColor.Green, UIColor.Magenta, UIColor.Orange };

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var view = new UIView();
                view.BackgroundColor = colors[i];
                view.ClipsToBounds = true;
                view.TranslatesAutoresizingMaskIntoConstraints = false;

                var imageView = new UIImageView();
                imageView.TranslatesAutoresizingMaskIntoConstraints = false;
                var image = UIImage.FromBundle(item.ImagePath);
                imageView.Image = image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

                var label = new UILabel();
                label.TranslatesAutoresizingMaskIntoConstraints = false;
                label.Text = item.Text;
                label.Font = UIFont.SystemFontOfSize(10);

                view.AddSubview(imageView);
                view.AddSubview(label);

                this.AddSubview(view);

                view.AddConstraints(new[]{
                    ConstraintExtensions.Height(view, 50)
                });

                view.AddConstraints(new[]{
                    ConstraintExtensions.CenterVertically(view, imageView),
                    ConstraintExtensions.CenterHorizontally(view, imageView),
                    ConstraintExtensions.CenterHorizontally(view, label),
                    ConstraintExtensions.TopToTop(label, imageView, 24)
                });
                this.AddConstraints(new [] {
                    ConstraintExtensions.EqualWidths(view, this, 0, 0.2f),
                });

                if (i % 4 == 0)
                {
                    //itemsInRow = 0;
                    this.AddConstraints(new[] {
                    ConstraintExtensions.LeadingToLeading(view, this,  0),
                    });

                }
                else
                {
                    this.AddConstraints(new[] {
                        ConstraintExtensions.HorizontalSpacing(view, previousView,  0),
                    });
                }
                previousView = view;
                //this.AddConstraints(new[] {
                //    ConstraintExtensions.LeadingToLeading(view, this,  itemsInRow * 50 * UIScreen.MainScreen.NativeScale),
                //});
                //itemsInRow++;

         


                if (i >= 4)
                {
                    this.AddConstraints(new[] {
                        ConstraintExtensions.TopToTop(view, this, 50),

                    });
                }
                else
                {
                    this.AddConstraints(new[] {
                             ConstraintExtensions.TopToTop(this, view, 0),
                    });
                }

                var recognizer = new UITapGestureRecognizer(() =>
                {
                    tabController.SelectedIndex = item.TabbarIndex;
                });
                // todo color selected item
                view.AddGestureRecognizer(recognizer);
            }
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

            });

            this.AddConstraints(new []{
                ConstraintExtensions.TopToTop(this, expandButton, 0),
                ConstraintExtensions.TrailingToTrailing(expandButton, this, 0),
                ConstraintExtensions.EqualWidths(expandButton, this, 0, 0.2f)
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
