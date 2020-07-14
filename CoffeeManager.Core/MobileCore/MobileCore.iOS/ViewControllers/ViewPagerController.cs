using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using MobileCore.iOS.Common;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using MvvmCross.Platform.iOS.Views;
using UIKit;

namespace MobileCore.iOS.ViewControllers
{
    public abstract class ViewPagerController : MvxEventSourcePageViewController, IMvxIosView, IUICollectionViewDelegate, IUICollectionViewDataSource, IUICollectionViewDelegateFlowLayout
    {
        private const float TitlesBarHeight = 40.0f;
        private const float SeparatorHeight = 1.0f;
        private const float ItemSpacing = 0.0f;
        private PageTitleViewCell widthCalculationCell;

        private UICollectionView titlesCollectionView;
        private int currentViewControllerIndex;
        private IEnumerable<object> titlesItemSource;

        public ViewPagerController() : base(UIPageViewControllerTransitionStyle.Scroll, UIPageViewControllerNavigationOrientation.Horizontal, UIPageViewControllerSpineLocation.None)
        {
            this.AdaptForBinding();
        }

        public ViewPagerController(IntPtr handle) : base(handle)
        {
            this.AdaptForBinding();
        }

        public List<UIViewController> ContentViewControllers { get; private set; }

        public MvxViewModelRequest Request { get; set; }

        public IMvxBindingContext BindingContext { get; set; }

        public IEnumerable<object> TitlesItemSource
        {
            get { return titlesItemSource; }
            set
            {
                titlesItemSource = value;
                titlesCollectionView.ReloadData();
            }
        }

        // Use only for binding
        public int CurrentIndex
        {
            set { SetCurrentViewController(value); }
        }

        public object DataContext
        {
            get { return BindingContext.DataContext; }
            set { BindingContext.DataContext = value; }
        }

        public IMvxViewModel ViewModel
        {
            get { return DataContext as IMvxViewModel; }
            set { DataContext = value; }
        }

        public override void ViewDidLoad()
		{
            base.ViewDidLoad();

            ViewModel?.ViewCreated();
            ContentViewControllers = new List<UIViewController>();
            SetupTitlesCollection();
            SetupSeparatorView();
            DoBind();

            GetNextViewController = NextViewController;
            GetPreviousViewController = PreviousViewController;
            DidFinishAnimating += DidFinishAnimation;
		}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ViewModel?.ViewAppearing();
        }

		public override void ViewDidAppear(bool animated)
		{
            base.ViewDidAppear(animated);
            ViewModel?.ViewAppeared();
            titlesCollectionView.SelectItem(NSIndexPath.FromRowSection(currentViewControllerIndex, 0), false, UICollectionViewScrollPosition.CenteredHorizontally);
		}

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            ViewModel?.ViewDisappearing();
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            ViewModel?.ViewDisappeared();
        }

        public override void DidMoveToParentViewController(UIViewController parent)
        {
            base.DidMoveToParentViewController(parent);
            if (parent == null)
            {
                ViewModel?.ViewDestroy();
            }
        }

		protected override void Dispose(bool disposing)
		{
            if (disposing)
            {
                DidFinishAnimating -= DidFinishAnimation;
            }

            base.Dispose(disposing);
		}

        public virtual nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            var itemsCount = TitlesItemSource?.Count() ?? 0;
            return itemsCount;
        }

        public virtual UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (PageTitleViewCell)collectionView.DequeueReusableCell(PageTitleViewCell.Key, indexPath);
            var title = (string)TitlesItemSource.ElementAt(indexPath.Row);
            cell.SetTitle(title);

            return cell;
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            var title = (string)TitlesItemSource.ElementAt(indexPath.Row);
            if (widthCalculationCell == null)
            {
                widthCalculationCell = PageTitleViewCell.Create();
            }

            widthCalculationCell.SetTitle(title);

            widthCalculationCell.Bounds = new CGRect(0, 0, (float)collectionView.Bounds.Width, (float)collectionView.Bounds.Height);

            widthCalculationCell.SetNeedsLayout();
            widthCalculationCell.LayoutIfNeeded();

            var width = widthCalculationCell.ContentView.SystemLayoutSizeFittingSize(UIView.UILayoutFittingCompressedSize).Width;

            return new CGSize(width, 40);
        }

        private UIViewController NextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            var currentIndex = ContentViewControllers.IndexOf(referenceViewController);
            if (currentIndex == ContentViewControllers.Count - 1)
            {
                return null;
            }

            var nextViewController = ContentViewControllers[currentIndex + 1];
            return nextViewController;
        }

        private UIViewController PreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            var currentIndex = ContentViewControllers.IndexOf(referenceViewController);
            if (currentIndex == 0)
            {
                return null;
            }

            var previousViewController = ContentViewControllers[currentIndex - 1];
            return previousViewController;
        }

        private void SetCurrentViewController(int index, UIPageViewControllerNavigationDirection direction = UIPageViewControllerNavigationDirection.Forward, bool animated = false)
        {
            var currentViewController = ContentViewControllers.ElementAtOrDefault(index);
            if (currentViewController != null)
            {
                AdaptChildViewControllerViewIfNeeded(currentViewController);
                SetViewControllers(new[] { currentViewController }, direction, animated, null);
                currentViewControllerIndex = index;
            }
        }
        
        private void AdaptChildViewControllerViewIfNeeded(UIViewController childViewController)
        {
            if (childViewController.View.Frame.Size != View.Frame.Size)
            {
                var newFrame = childViewController.View.Frame;
                newFrame.Size = this.View.Frame.Size;
                childViewController.View.Frame = newFrame;
                childViewController.View.LayoutIfNeeded();
            }
        }

        private void SetupTitlesCollection()
        {
            var layout = new UICollectionViewLayout();
            var flowLayout = new UICollectionViewFlowLayout();
            flowLayout.MinimumInteritemSpacing = ItemSpacing;
            flowLayout.MinimumLineSpacing = ItemSpacing;
            flowLayout.ScrollDirection = UICollectionViewScrollDirection.Horizontal;
            titlesCollectionView = new UICollectionView(CGRect.Empty, layout);
            titlesCollectionView.TranslatesAutoresizingMaskIntoConstraints = false;
            titlesCollectionView.ShowsHorizontalScrollIndicator = false;
            titlesCollectionView.Bounces = false;
            titlesCollectionView.CollectionViewLayout = flowLayout;
            titlesCollectionView.RegisterNibForCell(PageTitleViewCell.Nib, PageTitleViewCell.Key);
            titlesCollectionView.DataSource = this;
            titlesCollectionView.Delegate = this;
            titlesCollectionView.BackgroundColor = Colors.White;

            View.AddSubview(titlesCollectionView);

            NSLayoutConstraint.ActivateConstraints(new NSLayoutConstraint[]
            {
                titlesCollectionView.TopAnchor.ConstraintEqualTo(View.TopAnchor),
                titlesCollectionView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
                titlesCollectionView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
                titlesCollectionView.HeightAnchor.ConstraintEqualTo(TitlesBarHeight)
            });
        }

        private void SetupSeparatorView()
        {
            var separatorView = new UIView { BackgroundColor = Colors.VeryLightGray, TranslatesAutoresizingMaskIntoConstraints = false };

            View.AddSubview(separatorView);

            NSLayoutConstraint.ActivateConstraints(new NSLayoutConstraint[]
            {
                separatorView.TopAnchor.ConstraintEqualTo(titlesCollectionView.BottomAnchor),
                separatorView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
                separatorView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
                separatorView.HeightAnchor.ConstraintEqualTo(SeparatorHeight)
            });
        }

		private void DidFinishAnimation(object sender, UIPageViewFinishedAnimationEventArgs args)
        {
            if (args.Completed)
            {
                currentViewControllerIndex = ContentViewControllers.IndexOf(ViewControllers.FirstOrDefault());
                titlesCollectionView.SelectItem(NSIndexPath.FromRowSection(currentViewControllerIndex, 0), true, UICollectionViewScrollPosition.CenteredHorizontally);
            }
        }

        [Export("collectionView:didSelectItemAtIndexPath:")]
        private void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var navigationDirection = indexPath.Row > currentViewControllerIndex
                                      ? UIPageViewControllerNavigationDirection.Forward
                                      : UIPageViewControllerNavigationDirection.Reverse;

            SetCurrentViewController(indexPath.Row, navigationDirection, true);
            collectionView.SelectItem(indexPath, true, UICollectionViewScrollPosition.CenteredHorizontally);
        }
        
        

        protected virtual void DoBind()
        {
        }
    }

    public abstract class ViewPagerController<TViewModel> : ViewPagerController, IMvxIosView<TViewModel> where TViewModel : class, IMvxViewModel
    {
        public ViewPagerController()
        {
        }

        public ViewPagerController(IntPtr handle) : base(handle)
        {
        }

        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }

}