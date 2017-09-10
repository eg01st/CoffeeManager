using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;
using MvvmCross.Binding.iOS.Views.Gestures;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ShiftInfoCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("ShiftInfoCell");
        public static readonly UINib Nib;

        static ShiftInfoCell()
        {
            Nib = UINib.FromName("ShiftInfoCell", NSBundle.MainBundle);
        }

        protected ShiftInfoCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ShiftInfoCell, ShiftItemViewModel>();
                set.Bind(DateLabel).To(vm => vm.Date);
                set.Bind(NameLabel).To(vm => vm.UserName);
                set.Bind(ShiftEarnedAmountLabel).To(vm => vm.EarnedAmount);
                set.Bind(ExpenseLabel).To(vm => vm.ExpenseAmount);
                set.Bind(RealAmountLabel).To(vm => vm.RealAmount);
                set.Bind(TotalLabel).To(vm => vm.TotalAmount);
                set.Bind(StartAmount).To(vm => vm.RealShiftAmount);
                set.Bind(CreditCardAmount).To(vm => vm.CreditCardAmount);
                set.Bind(StartAmount).For(o => o.TextColor).To(vm => vm.IsPositive)
                   .WithConversion(new GenericConverter<bool, UIColor>((arg) =>
               {
                   if (arg)
                   {
                       return UIColor.Green;
                   }
                   else
                   {
                       return UIColor.Red;
                   }
               }));
                set.Bind(this.Tap()).For(tap => tap.Command).To(vm => vm.ShowDetailsCommand);
                set.Apply();
            });
        }



    }
}
