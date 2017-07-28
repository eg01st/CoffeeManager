using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using System.Windows.Input;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ProductItemCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("ProductItemCell");
        public static readonly UINib Nib;

        static ProductItemCell()
        {
            Nib = UINib.FromName("ProductItemCell", NSBundle.MainBundle);
        }

        private ICommand _longPressCommand;
        public ICommand DeleteProductCommand
        {
            get { return _longPressCommand; }
            set
            {
                _longPressCommand = value;

            }
        }

        private ICommand _toggleIsActiveCommand;
        public ICommand ToggleIsActiveCommand
        {
            get { return _toggleIsActiveCommand; }
            set
            {
                _toggleIsActiveCommand = value;

            }
        }

        protected ProductItemCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            var longPressGesture = new UILongPressGestureRecognizer(() => DeleteProductCommand?.Execute(null));
            AddGestureRecognizer(longPressGesture);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ProductItemCell, ProductItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(CategoryLabel).To(vm => vm.Category);
                set.Bind(IsActiveSwitch).For(s => s.On).To(vm => vm.IsActive);
                set.Bind(this).For(t => t.DeleteProductCommand).To(vm => vm.DeleteProductCommand);
                set.Bind(this).For(t => t.ToggleIsActiveCommand).To(vm => vm.ToggleIsActiveCommand);
                set.Apply();    
                IsActiveSwitch.ValueChanged += (sender, e) => 
                {
                    ToggleIsActiveCommand.Execute(null);
                }; 
            });
        }
    }
}
