using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;

namespace CoffeeManagerAdmin.iOS
{
    public partial class CalculationItemViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("CalculationItemViewCell");
        public static readonly UINib Nib;

        static CalculationItemViewCell()
        {
            Nib = UINib.FromName("CalculationItemViewCell", NSBundle.MainBundle);
        }

        protected CalculationItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<CalculationItemViewCell, CalculationItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(QuantityLabel).To(vm => vm.Quantity);
                set.Bind(DeleteButton).To(vm => vm.DeleteCommand);
                set.Apply();
            });
        }
    }
}
