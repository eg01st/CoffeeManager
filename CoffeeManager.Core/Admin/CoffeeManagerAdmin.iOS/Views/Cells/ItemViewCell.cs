using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;
using MvvmCross.Binding.iOS.Views.Gestures;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ItemViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("ItemViewCell");
        public static readonly UINib Nib;

        static ItemViewCell()
        {
            Nib = UINib.FromName("ItemViewCell", NSBundle.MainBundle);
        }

        protected ItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ItemViewCell, ItemViewModel>();
                set.Bind(Name).To(vm => vm.Name);
                set.Apply();
            });
        }
    }
}
