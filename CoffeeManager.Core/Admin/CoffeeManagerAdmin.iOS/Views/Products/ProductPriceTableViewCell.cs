using System;
using CoffeeManagerAdmin.Core.ViewModels.Products;
using CoffeeManagerAdmin.iOS.Converters;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Products
{
    public partial class ProductPriceTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("ProductPriceTableViewCell");
        public static readonly UINib Nib;

        static ProductPriceTableViewCell()
        {
            Nib = UINib.FromName("ProductPriceTableViewCell", NSBundle.MainBundle);
        }

        protected ProductPriceTableViewCell(IntPtr handle) : base(handle)
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
            var set = this.CreateBindingSet<ProductPriceTableViewCell, ProductPriceItemViewModel>();
            set.Bind(CoffeeRoomNameLabel).To(vm => vm.CoffeeRoomName);
            set.Bind(PriceTextField).To(vm => vm.Price).WithConversion(new DecimalToStringConverter());
            set.Bind(DiscountPriceTextField).To(vm => vm.DiscountPrice).WithConversion(new DecimalToStringConverter());
            set.Apply();
        }
    }
}
