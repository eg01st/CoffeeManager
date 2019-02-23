using CoffeeManagerAdmin.Core.ViewModels.Statistic;
using CoffeeManagerAdmin.iOS.TableSources;
using CoffeeManagerAdmin.iOS.Views.Shifts;
using MobileCore.iOS;
using MobileCore.iOS.Presenters;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Statistic
{
    [PageChildViewPresentation]
    public partial class StatisticResultSubView : ViewControllerBase<StatisticResultSubViewModel>
    {
        private SimpleTableSource creaditCardSalesTableSource;
        private SalesStatisticTableSource salesTableSource;
        private SimpleTableSource expensesTableSource;

        public StatisticResultSubView() : base("StatisticResultSubView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Styles.ApplyNavigationControllerStyle(NavigationController, NavigationItem, UINavigationItemLargeTitleDisplayMode.Never);
            SetActiveTab(0);
        }

        protected override void InitStylesAndContent()
        {
            salesTableSource = new SalesStatisticTableSource(
            SalesTableView,
                   SaleStatisticViewCell.Key,
                   SaleStatisticViewCell.Nib,
                   SaleStatisticHeaderViewCell.Key,
                   SaleStatisticHeaderViewCell.Nib);
            SalesTableView.Source = salesTableSource;
            SalesTableView.RowHeight = UITableView.AutomaticDimension;
            SalesTableView.EstimatedRowHeight = 50;

            expensesTableSource = new SimpleTableSource(ExpensesTableView, ExpenseItemCell.Key, ExpenseItemCell.Nib);
            ExpensesTableView.Source = expensesTableSource;


            var saleTap = new UITapGestureRecognizer(() => SetActiveTab(0));
            var expenseTap = new UITapGestureRecognizer(() => SetActiveTab(1));
            SalesHeaderView.AddGestureRecognizer(saleTap);
            ExpensesHeaderView.AddGestureRecognizer(expenseTap);
        }

        protected override void DoBind()
        {
            var set = this.CreateBindingSet<StatisticResultSubView, StatisticResultSubViewModel>();
            set.Bind(expensesTableSource).To(vm => vm.ExpensesVm.Items);
            set.Bind(salesTableSource).To(vm => vm.SalesVm.ItemsCollection);
            set.Bind(salesTableSource).For(t => t.SelectionChangedCommand).To(vm => vm.SalesVm.ItemSelectedCommand);
            set.Apply();
        }

        private void SetActiveTab(int index)
        {
            switch(index)
            {
                case(0):
                    SalesView.Hidden = false;
                    SalesHeaderView.BackgroundColor = UIColor.LightGray;

                    ExpensesView.Hidden = true;
                    ExpensesHeaderView.BackgroundColor = UIColor.White;

                    break;
                case(1):
                    SalesView.Hidden = true;
                    SalesHeaderView.BackgroundColor = UIColor.White;

                    ExpensesView.Hidden = false;
                    ExpensesHeaderView.BackgroundColor = UIColor.LightGray;
                    break;
                    
            }
        }

    }
}

