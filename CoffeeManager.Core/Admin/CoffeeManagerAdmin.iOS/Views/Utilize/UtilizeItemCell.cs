using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.iOS.Converters;

namespace CoffeeManagerAdmin.iOS
{
    public partial class UtilizeItemCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("UtilizeItemCell");
        public static readonly UINib Nib;

        static UtilizeItemCell()
        {
            Nib = UINib.FromName("UtilizeItemCell", NSBundle.MainBundle);
        }

        protected UtilizeItemCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() => 
            {
                var set = this.CreateBindingSet<UtilizeItemCell, UtilizeItemViewModel>();
                set.Bind(DateLabel).To(vm => vm.Date);
                set.Bind(NameLabel).To(vm => vm.Name); 
                set.Bind(QuantityLabel).To(vm => vm.Quantity).WithConversion(new DecimalToStringConverter());
                set.Apply();
            });
        }
    }
}
