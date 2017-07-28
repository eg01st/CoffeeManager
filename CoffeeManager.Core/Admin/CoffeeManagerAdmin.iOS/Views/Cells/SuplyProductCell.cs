using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using MvvmCross.Platform.Converters;
using CoffeeManagerAdmin.Core.ViewModels;
using MvvmCross.Binding.iOS.Views.Gestures;
using System.Windows.Input;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SuplyProductCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("SuplyProductCell");
        public static readonly UINib Nib;

        private ICommand _longPressCommand;
        public ICommand RequestSuplyCommand
        {
            get { return _longPressCommand; }
            set
            {
                _longPressCommand = value;

            }
        }

        static SuplyProductCell()
        {
            Nib = UINib.FromName("SuplyProductCell", NSBundle.MainBundle);
        }

        protected SuplyProductCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.

        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();


            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<SuplyProductCell, SuplyProductItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(AmountLabel).To(vm => vm.Quatity);
                set.Bind(PriceLabel).To(vm => vm.Price).WithConversion(new DecimalToStringConverter());
                set.Bind(this.Tap()).For(tap => tap.Command).To(vm => vm.SelectCommand);
                set.Apply();

            });
        }
    }
}
