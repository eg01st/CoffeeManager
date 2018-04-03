using System;
using CoffeeManagerAdmin.Core.ViewModels;
using CoffeeManagerAdmin.Core.ViewModels.SuplyProducts;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class MappedSuplyProductsTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("MappedSuplyProductsTableViewCell");
        public static readonly UINib Nib;

        static MappedSuplyProductsTableViewCell()
        {
            Nib = UINib.FromName("MappedSuplyProductsTableViewCell", NSBundle.MainBundle);
        }

        protected MappedSuplyProductsTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<MappedSuplyProductsTableViewCell, SuplyProductItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Apply();
            }
            );
        }
    }
}
