using System;
using CoffeeManagerAdmin.Core;
using Foundation;
using MobileCore.iOS;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class StatisticSaleItemViewCell : BaseTableViewCell
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

        protected override void DoBind()
        {
            base.DoBind();
            var set = this.CreateBindingSet<StatisticSaleItemViewCell, StatisticSaleItemViewModel>();
            set.Bind(NameLabel).To(vm => vm.Name);
            set.Bind(PriceLabel).To(vm => vm.Amount);
            set.Apply();
        }
    }
}
