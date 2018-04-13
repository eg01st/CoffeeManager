using System;

using UIKit;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using System.Collections.Generic;
using CoffeeManagerAdmin.iOS.Converters;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using CoffeeManagerAdmin.iOS.Views.Shifts;

namespace CoffeeManagerAdmin.iOS
{
    public partial class StatisticResultView : ViewControllerBase<StatisticResultViewModel>
    {
        private SimpleTableSource creaditCardSalesTableSource;
        private SalesStatisticTableSource salesTableSource;
        private SimpleTableSource expensesTableSource;

        public StatisticResultView() : base("StatisticResultView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Статистика";

            //var btn = new UIBarButtonItem();
            //btn.Title = "Графики";

            //NavigationItem.SetRightBarButtonItem(btn, false);
            //this.AddBindings(new Dictionary<object, string>
            //{
            //    {btn, "Clicked ShowChartCommand"},

            //});

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

            creaditCardSalesTableSource = new SimpleTableSource(CrediCardTableView, StatisticSaleItemViewCell.Key, StatisticSaleItemViewCell.Nib);
            CrediCardTableView.Source = creaditCardSalesTableSource;

            expensesTableSource = new SimpleTableSource(ExpensesTableView, ExpenseItemCell.Key, ExpenseItemCell.Nib);
            ExpensesTableView.Source = expensesTableSource;


            var saleTap = new UITapGestureRecognizer(() => SetActiveTab(0));
            var expenseTap = new UITapGestureRecognizer(() => SetActiveTab(1));
            var creditCardTap = new UITapGestureRecognizer(() => SetActiveTab(2));
            SalesHeaderView.AddGestureRecognizer(saleTap);
            ExpensesHeaderView.AddGestureRecognizer(expenseTap);
            CreaditCardHeaderView.AddGestureRecognizer(creditCardTap);
        }

        protected override void DoBind()
        {
            var set = this.CreateBindingSet<StatisticResultView, StatisticResultViewModel>();
            set.Bind(CreditCardEntireAmountLabel).To(vm => vm.CreditCardSalesVm.EntireSaleAmount).WithConversion(new DecimalToStringConverter());
            set.Bind(creaditCardSalesTableSource).To(vm => vm.CreditCardSalesVm.Sales);
            set.Bind(expensesTableSource).To(vm => vm.ExpensesVm.Items);
            set.Bind(salesTableSource).To(vm => vm.SalesVm.Items);
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

                    CreditCardView.Hidden = true;
                    CreaditCardHeaderView.BackgroundColor = UIColor.White;

                    break;
                case(1):
                    SalesView.Hidden = true;
                    SalesHeaderView.BackgroundColor = UIColor.White;

                    ExpensesView.Hidden = false;
                    ExpensesHeaderView.BackgroundColor = UIColor.LightGray;

                    CreditCardView.Hidden = true;
                    CreaditCardHeaderView.BackgroundColor = UIColor.White;
                    break;
                 case(2):
                    SalesView.Hidden = true;
                    SalesHeaderView.BackgroundColor = UIColor.White;

                    ExpensesView.Hidden = true;
                    ExpensesHeaderView.BackgroundColor = UIColor.White;

                    CreditCardView.Hidden = false;
                    CreaditCardHeaderView.BackgroundColor = UIColor.LightGray;
                    break;
                    
            }
        }

    }
}

