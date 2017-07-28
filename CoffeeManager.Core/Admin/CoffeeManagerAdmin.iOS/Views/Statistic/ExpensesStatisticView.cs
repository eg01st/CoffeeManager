using System;

using UIKit;
using MvvmCross.iOS.Views;
using CoffeeManagerAdmin.Core.ViewModels.Statistic;
using MvvmCross.Binding.BindingContext;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ExpensesStatisticView : MvxViewController
    {
        public ExpensesStatisticView() : base("ExpensesStatisticView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Траты";
            var source = new ExpenseTableSource(TableView);
            TableView.Source = source;

            var set = this.CreateBindingSet<ExpensesStatisticView, ExpensesStatisticViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }
    }
}

