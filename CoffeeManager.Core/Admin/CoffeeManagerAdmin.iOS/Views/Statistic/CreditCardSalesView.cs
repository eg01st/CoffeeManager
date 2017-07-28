using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class CreditCardSalesView : MvxViewController
    {
        public CreditCardSalesView() : base("CreditCardSalesView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Продажи по кредитке";
            var salesTableSource = new SaleTableSource(TableView);
            TableView.Source = salesTableSource;
            TableView.RegisterNibForCellReuse(SaleItemCell.Nib, SaleItemCell.Key);

            var set = this.CreateBindingSet<CreditCardSalesView, CreditCardSalesViewModel>();
            set.Bind(EntireAmountLabel).To(vm => vm.EntireSaleAmount).WithConversion(new DecimalToStringConverter());
            set.Bind(salesTableSource).To(vm => vm.Sales);
            set.Apply();
        }
    }
}

