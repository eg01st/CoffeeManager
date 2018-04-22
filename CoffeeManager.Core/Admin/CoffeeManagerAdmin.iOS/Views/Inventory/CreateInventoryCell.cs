using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.Core.ViewModels.Inventory.Create;

namespace CoffeeManagerAdmin.iOS
{
    public partial class CreateInventoryCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("CreateInventoryCell");
        public static readonly UINib Nib;

        static CreateInventoryCell()
        {
            Nib = UINib.FromName("CreateInventoryCell", NSBundle.MainBundle);
        }

        protected CreateInventoryCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<CreateInventoryCell, CreateInventoryItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(PreviosCountLabel).To(vm => vm.QuantityBefore);
                set.Bind(CurrentCountLabel).To(vm => vm.QuantityAfter);
                set.Bind(IsSelectedImage).For(i => i.Hidden).To(vm => vm.IsProceeded).WithConversion(new InvertedBoolConverter());
                set.Apply();
            });
        }
    }
}
