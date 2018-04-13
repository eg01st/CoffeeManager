using CoffeeManagerAdmin.Core.ViewModels.Home;
using CoreGraphics;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Support.XamarinSidebar;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Home.SideMenu
{
    [MvxSidebarPresentation(MvxPanelEnum.Left, MvxPanelHintType.PushPanel, false)]
    public partial class SideMenuView : BaseMenuViewController<SideMenuViewModel>
    {
        private const float EstimatedRowHeight = 8.0f;
        private MenuTableSource source;

        public override UIImage MenuButtonImage => UIImage.FromBundle("ic_hamburger_black").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

        public SideMenuView() : base("SideMenuView", null)
        {
        }

        protected override void InitStylesAndContent()
        {
            
        }

        protected override void DoViewDidLoad()
        {
            base.DoViewDidLoad();

            InitTableView();
        }

        private void InitTableView()
        {
            var tableView = MenuTableView;
            tableView.TableFooterView = new UIView(CGRect.Empty);
            source = new MenuTableSource(tableView);
            tableView.Source = source;
            tableView.RowHeight = UITableView.AutomaticDimension;
            tableView.EstimatedRowHeight = EstimatedRowHeight;
            tableView.BackgroundColor = UIColor.White;

            tableView.RegisterNibForCellReuse(MenuCell.Nib, MenuCell.Key);
            tableView.RegisterNibForCellReuse(MenuHeaderCell.Nib, MenuHeaderCell.Key);
        }

        protected override void DoBind()
        {
            base.DoBind();

            var bindingSet = this.CreateBindingSet<SideMenuView, SideMenuViewModel>();

            bindingSet.Bind(source).For(v => v.ItemsSource).To(vm => vm.ItemsCollection);
            bindingSet.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);

            bindingSet.Apply();
        }
    }
}

