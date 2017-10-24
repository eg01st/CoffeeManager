using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using System.Windows.Input;

namespace CoffeeManagerAdmin.iOS
{
    public partial class TransferProductItemCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("TransferProductItemCell");
        public static readonly UINib Nib;


        private ICommand discardTransferCommand;
        public ICommand DeleteCommand
        {
            get { return discardTransferCommand; }
            set
            {
                discardTransferCommand = value;
            }
        }

        static TransferProductItemCell()
        {
            Nib = UINib.FromName("TransferProductItemCell", NSBundle.MainBundle);

        }

        protected TransferProductItemCell(IntPtr handle) : base(handle)
        {
            var longPressGesture = new UILongPressGestureRecognizer((sender) =>
            {
                if (sender.State == UIGestureRecognizerState.Began)
                {
                    DeleteCommand?.Execute(null);
                }
            });

            AddGestureRecognizer(longPressGesture);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() => 
            {
                var set = this.CreateBindingSet<TransferProductItemCell, SelectSuplyProductItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(QuantityLabel).To(vm => vm.QuantityToTransfer);
                set.Bind(IsSelectedImage).For(i => i.Hidden).To(vm => vm.IsSelected).WithConversion(new InvertedBoolConverter());
                set.Bind(this).For(t => t.DeleteCommand).To(vm => vm.DiscardTransferCommand);
                set.Apply();
            });
        }
    }
}
