using CoffeeManagerAdmin.Core.ViewModels.Statistic;
using MobileCore.iOS;
using MobileCore.iOS.Presenters;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Statistic
{
    [NonSwipeBackNavigation]
    [PageRootViewPresentation]
    public class StatisticResultParentView : ViewPagerController<StatisticResultParentViewModel>
    {
        public StatisticResultParentView()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Styles.ApplyNavigationControllerStyle(NavigationController, NavigationItem, UINavigationItemLargeTitleDisplayMode.Never);
            ViewModel.ShowInitialsViewModels();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

          //  NavigationItem.BackBarButtonItem = new UIBarButtonItem(string.Empty, UIBarButtonItemStyle.Plain, null);

            ExtendedLayoutIncludesOpaqueBars = true;
            EdgesForExtendedLayout = UIRectEdge.None;
        }

        protected override void DoBind()
        {
            var bindingSet = this.CreateBindingSet<StatisticResultParentView, StatisticResultParentViewModel>();

          // bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(this).For(v => v.TitlesItemSource).To(vm => vm.Titles);
           // bindingSet.Bind(this).For(nameof(CurrentIndex)).To(vm => vm.CategoryIndex);

            bindingSet.Apply();
        }
    }
}
