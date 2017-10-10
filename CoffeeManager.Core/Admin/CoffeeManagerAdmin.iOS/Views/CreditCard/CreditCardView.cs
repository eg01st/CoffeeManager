using System;
using CoffeeManagerAdmin.Core;
using UIKit;
using MvvmCross.Binding.BindingContext;

namespace CoffeeManagerAdmin.iOS
{
    public partial class CreditCardView : ViewControllerBase<CreditCardViewModel>
    {
        private SimpleTableSource tableSource;

        public CreditCardView() : base("CreditCardView", null)
        {
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            Title = "Кредитка";

            SaveCurrentAmountButton.TouchUpInside += (sender, e) => View.EndEditing(true);
            CashoutButton.TouchUpInside += (sender, e) => View.EndEditing(true);

            tableSource = new SimpleTableSource(CashoutTableView, CashoutTableViewCell.Key, CashoutTableViewCell.Nib, CashoutTableHeader.Key, CashoutTableHeader.Nib);
            CashoutTableView.Source = tableSource;
        }

        protected override void DoBind()
        {
            var set = this.CreateBindingSet<CreditCardView, CreditCardViewModel>();
            set.Bind(CurrentAmountTextField).To(vm => vm.CurrentAmount);
            set.Bind(CashoutAmountTextField).To(vm => vm.AmountToCashOut);
            set.Bind(SaveCurrentAmountButton).To(vm => vm.SetAmountCommand);
            set.Bind(CashoutButton).To(vm => vm.CashoutCommand);
            set.Bind(tableSource).To(vm => vm.CashoutItems);
            set.Apply();
        }

    }
}

