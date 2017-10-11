using System;
using CoffeeManagerAdmin.Core;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ExpenseTypeDetailsView : ViewControllerBase<ExpenseTypeDetailsViewModel>
    {
        public ExpenseTypeDetailsView() : base("ExpenseTypeDetailsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Детали расхода";
            var source = new SimpleTableSource(MappedSuplyProductsTableView, MappedSuplyProductsTableViewCell.Key, MappedSuplyProductsTableViewCell.Nib);
            MappedSuplyProductsTableView.Source = source;

            var set = this.CreateBindingSet<ExpenseTypeDetailsView, ExpenseTypeDetailsViewModel>();
            set.Bind(source).To(vm => vm.MappedItems);
            set.Bind(ExpenseTypeNameLabel).To(vm => vm.ExpenseName);
            set.Bind(MapSuplyProductButton).To(vm => vm.MapSuplyProductCommand);
            set.Apply();
        }
    }
}

