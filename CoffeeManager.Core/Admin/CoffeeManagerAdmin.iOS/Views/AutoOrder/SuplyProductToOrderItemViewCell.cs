using System;
using CoffeeManagerAdmin.Core.ViewModels.AutoOrder;
using Foundation;
using MobileCore.iOS;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.AutoOrder
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

        public IMvxCommand ToggleShouldUpdateQuantityBeforeOrderCommand { get; set; }

        protected override void DoBind()
        {
            base.DoBind();
            var set = this.CreateBindingSet<SuplyProductToOrderItemViewCell, SuplyProductToOrderItemViewModel>();
            set.Bind(SuplyProductNameLabel).To(vm => vm.SuplyProductName);
            set.Bind(QuantityAfterTextField).To(vm => vm.QuantityShouldBeAfterOrder);
            set.Bind(QuantityAfterTextField).For(i => i.Enabled).To(vm => vm.IsEditable);
            set.Bind(ShouldUpdateQuantityBeforeOrderSwitch).For(s => s.On).To(vm => vm.ShouldUpdateQuantityBeforeOrder);
            set.Bind(this).For(t => t.ToggleShouldUpdateQuantityBeforeOrderCommand).To(vm => vm.ToggleShouldUpdateQuantityBeforeOrderCommand);
            set.Apply();
            ShouldUpdateQuantityBeforeOrderSwitch.ValueChanged += (sender, e) =>
            {
                ToggleShouldUpdateQuantityBeforeOrderCommand.Execute(null);
            };
        }
    }
}
