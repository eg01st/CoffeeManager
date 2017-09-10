using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManager.Models;

namespace CoffeeManagerAdmin.iOS
{
    public partial class GroupedSaleCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("GroupedSaleCell");
        public static readonly UINib Nib;

        static GroupedSaleCell()
        {
            Nib = UINib.FromName("GroupedSaleCell", NSBundle.MainBundle);
        }

        protected GroupedSaleCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<GroupedSaleCell, Entity>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(CountLabel).To(vm => vm.Id);
                set.Apply();
            });
        }
    }
}
