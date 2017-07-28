using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SaleStatisticViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("SaleStatisticViewCell");
        public static readonly UINib Nib;

        static SaleStatisticViewCell()
        {
            Nib = UINib.FromName("SaleStatisticViewCell", NSBundle.MainBundle);
        }

        protected SaleStatisticViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            this.DelayBind(() =>
            {
                var conv = new GenericConverter<bool, bool>((arg) => !arg);

                var set = this.CreateBindingSet<SaleStatisticViewCell, SaleItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(AmountLabel).To(vm => vm.Amount);
                set.Bind(QuantityLabel).To(vm => vm.Quantity);
                set.Apply();
            });
        }
    }
}
