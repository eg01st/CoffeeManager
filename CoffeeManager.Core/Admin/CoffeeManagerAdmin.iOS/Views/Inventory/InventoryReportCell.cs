using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class InventoryReportCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("InventoryReportCell");
        public static readonly UINib Nib;

        static InventoryReportCell()
        {
            Nib = UINib.FromName("InventoryReportCell", NSBundle.MainBundle);
        }

        protected InventoryReportCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() => 
            {
                var set = this.CreateBindingSet<InventoryReportCell, InventoryItemViewModel>();
                set.Bind(DateLabel).To(vm => vm.Date);
                set.Apply();
            });
        }
    }
}
