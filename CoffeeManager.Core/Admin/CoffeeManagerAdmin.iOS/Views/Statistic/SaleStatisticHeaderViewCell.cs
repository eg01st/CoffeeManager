using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SaleStatisticHeaderViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("SaleStatisticHeaderViewCell");
        public static readonly UINib Nib;

        static SaleStatisticHeaderViewCell()
        {
            Nib = UINib.FromName("SaleStatisticHeaderViewCell", NSBundle.MainBundle);
        }

        protected SaleStatisticHeaderViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() => 
            {
                var set = this.CreateBindingSet<SaleStatisticHeaderViewCell, StatisticSaleHeaderViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(AmountLabel).To(vm => vm.Amount).WithConversion(new DecimalToStringConverter());
                set.Apply();
            });
        }
    }
}
