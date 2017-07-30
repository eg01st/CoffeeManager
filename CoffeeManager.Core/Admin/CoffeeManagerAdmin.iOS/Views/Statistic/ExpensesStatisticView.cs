using System;

using UIKit;
using MvvmCross.iOS.Views;
using CoffeeManagerAdmin.Core.ViewModels.Statistic;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ExpensesStatisticView : SearchViewController<ExpensesStatisticView, ExpensesStatisticViewModel, ExpenseItemViewModel>
    {
        public ExpensesStatisticView() : base("ExpensesStatisticView", null)
        {
        }

        protected override SimpleTableSource TableSource => new SimpleTableSource(TableView, ExpenseItemCell.Key, ExpenseItemCell.Nib);
     

        protected override UIView TableViewContainer=> ContainerView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Траты";
        }

        protected override MvxFluentBindingDescriptionSet<ExpensesStatisticView, ExpensesStatisticViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<ExpensesStatisticView, ExpensesStatisticViewModel>();
        }
    }
}

