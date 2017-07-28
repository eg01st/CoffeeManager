using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels.Orders;
using MvvmCross.Binding.iOS.Views.Gestures;
using System.Windows.Input;

namespace CoffeeManagerAdmin.iOS
{
    public partial class OrderViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("OrderViewCell");
        public static readonly UINib Nib;

        static OrderViewCell()
        {
            Nib = UINib.FromName("OrderViewCell", NSBundle.MainBundle);
        }

        private ICommand _longPressCommand;
        public ICommand DeleteOrderCommand
        {
            get { return _longPressCommand; }
            set
            {
                _longPressCommand = value;

            }
        }

        protected OrderViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            var longPressGesture = new UILongPressGestureRecognizer(() => DeleteOrderCommand?.Execute(null));
            AddGestureRecognizer(longPressGesture);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<OrderViewCell, OrderViewModel>();
                set.Bind(DisplayLabel).To(vm => vm.DisplayName);
                set.Bind(StatusLabel).To(vm => vm.Status);
                set.Bind(this).For(t => t.DeleteOrderCommand).To(vm => vm.DeleteOrderCommand);
                set.Apply();
            });
        }
    }
}
