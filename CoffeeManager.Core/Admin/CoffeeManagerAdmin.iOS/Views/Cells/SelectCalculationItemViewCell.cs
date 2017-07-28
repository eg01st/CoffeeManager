using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;
using MvvmCross.Binding.iOS.Views.Gestures;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SelectCalculationItemViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("SelectCalculationItemViewCell");
        public static readonly UINib Nib;

        static SelectCalculationItemViewCell()
        {
            Nib = UINib.FromName("SelectCalculationItemViewCell", NSBundle.MainBundle);
        }

        protected SelectCalculationItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<SelectCalculationItemViewCell, SelectCalculationItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Apply();
            });
        }
    }
}
