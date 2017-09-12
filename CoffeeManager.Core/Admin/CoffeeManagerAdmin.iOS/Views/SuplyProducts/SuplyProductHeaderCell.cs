using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SuplyProductHeaderCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("SuplyProductHeaderCell");
        public static readonly UINib Nib;

        static SuplyProductHeaderCell()
        {
            Nib = UINib.FromName("SuplyProductHeaderCell", NSBundle.MainBundle);
        }

        protected SuplyProductHeaderCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<SuplyProductHeaderCell, ExpenseTypeHeaderViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Apply();
            });
        }
    }
}
