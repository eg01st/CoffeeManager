using System;
using CoffeeManagerAdmin.Core.ViewModels.Products;
using CoffeeManagerAdmin.iOS.Converters;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Products
{
    public partial class PaymentStrategyTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("PaymentStrategyTableViewCell");
        public static readonly UINib Nib;

        static PaymentStrategyTableViewCell()
        {
            Nib = UINib.FromName("PaymentStrategyTableViewCell", NSBundle.MainBundle);
        }

        protected PaymentStrategyTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(DoBind);
        }

        private void DoBind()
        {
            var set = this.CreateBindingSet<PaymentStrategyTableViewCell, ProductPaymentStrategyItemViewModel>();
            set.Bind(CoffeeRoomNameLabel).To(vm => vm.CoffeeRoomName);
            set.Bind(DayPercentTextField).To(vm => vm.DayShiftPersent);
            set.Bind(NightPercentTextField).To(vm => vm.NightShiftPercent);
            set.Apply();
        }
    }
}
