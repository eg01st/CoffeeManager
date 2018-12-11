using System;

using Foundation;
using UIKit;
using MobileCore.iOS;
using CoffeeManagerAdmin.Core.ViewModels.AutoOrder;
using MvvmCross.Binding.BindingContext;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SuplyProductToOrderItemViewCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString("SuplyProductToOrderItemViewCell");
        public static readonly UINib Nib;

        static SuplyProductToOrderItemViewCell()
        {
            Nib = UINib.FromName("SuplyProductToOrderItemViewCell", NSBundle.MainBundle);
        }

        protected SuplyProductToOrderItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected override void DoBind()
        {
            base.DoBind();
            var set = this.CreateBindingSet<SuplyProductToOrderItemViewCell, SuplyProductToOrderItemViewModel>();
            set.Bind(SuplyProductNameLabel).To(vm => vm.SuplyProductName);
            set.Bind(QuantityAfterTextField).To(vm => vm.QuantityShouldBeAfterOrder);
            set.Bind(QuantityAfterTextField).For(i => i.Enabled).To(vm => vm.IsEditable);
            set.Apply();
        }
    }
}
