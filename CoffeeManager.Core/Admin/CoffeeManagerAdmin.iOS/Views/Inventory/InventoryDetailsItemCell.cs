using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.iOS.Converters;

namespace CoffeeManagerAdmin.iOS
{
    public partial class InventoryDetailsItemCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("InventoryDetailsItemCell");
        public static readonly UINib Nib;

        static InventoryDetailsItemCell()
        {
            Nib = UINib.FromName("InventoryDetailsItemCell", NSBundle.MainBundle);
        }

        protected InventoryDetailsItemCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<InventoryDetailsItemCell, InventoryReportDetailsItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.SuplyProductName);
                set.Bind(BeforeLabel).To(vm => vm.QuantityBefore).WithConversion(new DecimalToStringConverter());;
                set.Bind(AfterLabel).To(vm => vm.QuantityAfer).WithConversion(new DecimalToStringConverter());;
                set.Bind(DiffLabel).To(vm => vm.QuantityDiff).WithConversion(new DecimalToStringConverter());;
                set.Apply();
            });
        }
    }
}
