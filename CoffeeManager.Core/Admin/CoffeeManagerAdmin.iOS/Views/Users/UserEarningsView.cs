using System;

using UIKit;
using CoffeManager.Common;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.Core.ViewModels.Users;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MobileCore.iOS.ViewControllers;

namespace CoffeeManagerAdmin.iOS
{
    public partial class UserEarningsView : ViewControllerBase<UserEarningsViewModel>
    {
        public UserEarningsView() : base("UserEarningsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            Title = "Зачисление зарплаты";

            var source = new SimpleTableSource(EarningsTableView, UserEarningCell.Key, UserEarningCell.Nib, UserEarningHeader.Key, UserEarningHeader.Nib);
            EarningsTableView.Source = source;

            var set = this.CreateBindingSet<UserEarningsView, UserEarningsViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }
    }
}

