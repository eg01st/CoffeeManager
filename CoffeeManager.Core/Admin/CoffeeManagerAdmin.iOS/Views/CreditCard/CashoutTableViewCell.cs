using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class CashoutTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("CashoutTableViewCell");
        public static readonly UINib Nib;

        static CashoutTableViewCell()
        {
            Nib = UINib.FromName("CashoutTableViewCell", NSBundle.MainBundle);
        }

        protected CashoutTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() => 
            {
                var set = this.CreateBindingSet<CashoutTableViewCell, CashoutHistoryItemViewModel>();
                set.Bind(AmountLabel).To(vm=> vm.Amount);
                set.Bind(DateLabel).To(vm => vm.Date);
                set.Apply();
            });
        }
    }
}
