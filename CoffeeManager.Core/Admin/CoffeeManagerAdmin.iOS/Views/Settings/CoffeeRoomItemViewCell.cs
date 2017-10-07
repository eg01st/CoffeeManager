using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class CoffeeRoomItemViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("CoffeeRoomItemViewCell");
        public static readonly UINib Nib;

        static CoffeeRoomItemViewCell()
        {
            Nib = UINib.FromName("CoffeeRoomItemViewCell", NSBundle.MainBundle);
        }

        protected CoffeeRoomItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<CoffeeRoomItemViewCell, CoffeeRoomItemViewModel>();
                set.Bind(CoffeeRoomNameLabel).To(vm => vm.Name);
                set.Apply();
            });
        }
    }
}
