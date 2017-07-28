using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels.Orders;
using MvvmCross.Binding.iOS.Views.Gestures;
using System.Windows.Input;
using MvvmCross.Platform.UI;
using MvvmCross.Plugins.Visibility;

namespace CoffeeManagerAdmin.iOS
{
    public partial class OrderItemViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("OrderItemViewCell");
        public static readonly UINib Nib;

        static OrderItemViewCell()
        {
            Nib = UINib.FromName("OrderItemViewCell", NSBundle.MainBundle);
        }

        protected OrderItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            var longPressGesture = new UILongPressGestureRecognizer(() => DeleteOrderItemCommand?.Execute(null));
            AddGestureRecognizer(longPressGesture);
        }

        private ICommand _longPressCommand;
        public ICommand DeleteOrderItemCommand
        {
            get { return _longPressCommand; }
            set
            {
                _longPressCommand = value;

            }
        }

        private bool _isDone;
        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                _isDone = value;
                if (!_isDone)
                {
                    DoneImageZeroHeight.Active = true;
                    DoneImageNormarHeight.Active = false;
                }
                else
                {
                    DoneImageZeroHeight.Active = false;
                    DoneImageNormarHeight.Active = true;
                }
            }
        }


        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<OrderItemViewCell, OrderItemViewModel>();
                set.Bind(NameText).To(vm => vm.SuplyProductName);
                set.Bind(QuantityText).To(vm => vm.Quantity);
                set.Bind(PriceLabel).To(vm => vm.Price)
                   .WithConversion(new DecimalToStringConverter());
                set.Bind(this).For(v => v.IsDone).To(vm => vm.IsDone);

                set.Bind(this.Tap()).For(t => t.Command).To(vm => vm.SaveItemCommand);
                set.Bind(this).For(t => t.DeleteOrderItemCommand).To(vm => vm.DeleteItemCommand);
                set.Apply();
            });
        }
    }
}
