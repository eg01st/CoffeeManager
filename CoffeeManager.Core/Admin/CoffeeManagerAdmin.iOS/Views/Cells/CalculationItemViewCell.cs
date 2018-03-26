using System;
using CoffeeManagerAdmin.Core.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Cells
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
