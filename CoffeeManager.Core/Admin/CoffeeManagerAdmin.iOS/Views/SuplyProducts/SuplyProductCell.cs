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

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set
            {
                deleteCommand = value;

            }
        }

        static SuplyProductCell()
        {
            Nib = UINib.FromName("SuplyProductCell", NSBundle.MainBundle);
        }

        protected SuplyProductCell(IntPtr handle) : base(handle)
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
                var set = this.CreateBindingSet<SuplyProductCell, SuplyProductItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(AmountLabel).To(vm => vm.Quatity);
                set.Bind(this).For(t => t.DeleteCommand).To(vm => vm.DeleteItemCommand);
                set.Apply();

            });
        }
    }
}
