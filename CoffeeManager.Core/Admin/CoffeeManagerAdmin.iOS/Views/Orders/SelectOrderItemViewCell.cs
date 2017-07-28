using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;
using MvvmCross.Binding.iOS.Views.Gestures;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.ViewModels.Orders;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SelectOrderItemViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("SelectOrderItemViewCell");
        public static readonly UINib Nib;

        private ICommand _selectActionCommand;
        public ICommand SelectActionCommand
        {
            get { return _selectActionCommand; }
            set
            {
                _selectActionCommand = value;

            }
        }


        private ICommand _longPressCommand;
        public ICommand DeleteItemCommand
        {
            get { return _longPressCommand; }
            set
            {
                _longPressCommand = value;

            }
        }

        static SelectOrderItemViewCell()
        {
            Nib = UINib.FromName("SelectOrderItemViewCell", NSBundle.MainBundle);
        }

        protected SelectOrderItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            var longPressGesture = new UILongPressGestureRecognizer(() => DeleteItemCommand?.Execute(null));
            AddGestureRecognizer(longPressGesture);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<SelectOrderItemViewCell, SelectOrderItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(IsSelected).To(vm => vm.IsSelected);
                set.Bind(this).For(t => t.SelectActionCommand).To(vm => vm.AddRequestItemCommand);
                set.Bind(this).For(t => t.DeleteItemCommand).To(vm => vm.DeleteItemCommand);
                set.Bind(CountLabel).To(vm => vm.Quantity);
                set.Apply();

                IsSelected.ValueChanged += (sender, e) =>
                 {
                     if (!IsSelected.Selected)
                     {
                         SelectActionCommand.Execute(null);
                     }
                 };
            });
        }
    }
}
