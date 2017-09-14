using System;

using UIKit;
using CoffeManager.Common;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class UserEarningsView : ViewControllerBase
    {
        public UserEarningsView() : base("UserEarningsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            Title = "Зачисление зарплаты";

            var source = new SimpleTableSourceWithHeader(EarningsTableView, UserEarningCell.Key, UserEarningCell.Nib, UserEarningHeader.Key);
            EarningsTableView.Source = source;
            EarningsTableView.RegisterNibForHeaderFooterViewReuse(UserEarningHeader.Nib, UserEarningHeader.Key);

            var set = this.CreateBindingSet<UserEarningsView, UserEarningsViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }
    }
}

