using System;
using CoffeeManagerAdmin.Core;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class StatisticSaleItemViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("StatisticSaleItemViewCell");
        public static readonly UINib Nib;

        static StatisticSaleItemViewCell()
        {
            Nib = UINib.FromName("StatisticSaleItemViewCell", NSBundle.MainBundle);
        }

        protected StatisticSaleItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<StatisticSaleItemViewCell, StatisticSaleItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(PriceLabel).To(vm => vm.Amount);
                set.Bind(TimeLabel).To(vm => vm.Time);
                set.Apply();
            });
        }
    }
}
