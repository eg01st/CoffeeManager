using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using System.Windows.Input;
using CoffeeManagerAdmin.iOS.Converters;

namespace CoffeeManagerAdmin.iOS
{
    public partial class UserPenaltyItemCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("UserPenaltyItemCell");
        public static readonly UINib Nib;

        static UserPenaltyItemCell()
        {
            Nib = UINib.FromName("UserPenaltyItemCell", NSBundle.MainBundle);
        }

        protected UserPenaltyItemCell(IntPtr handle) : base(handle)
        {
            var longPressGesture = new UILongPressGestureRecognizer((sender) =>
            {
                if(sender.State == UIGestureRecognizerState.Began)
                {
                    DismisPenaltyCommand?.Execute(null);
                }
            });
           
            AddGestureRecognizer(longPressGesture);
        }

        private ICommand _longPressCommand;
        public ICommand DismisPenaltyCommand
        {
            get { return _longPressCommand; }
            set
            {
                _longPressCommand = value;

            }
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() => 
            {
                var set = this.CreateBindingSet<UserPenaltyItemCell, UserPenaltyItemViewModel>();
                set.Bind(DateLabel).To(vm => vm.Date);
                set.Bind(AmountLabel).To(vm => vm.Amount).WithConversion(new DecimalToStringConverter());
                set.Bind(this).For(t => t.DismisPenaltyCommand).To(vm => vm.DismisPenaltyCommand);
                set.Apply();
            });
        }
    }
}
