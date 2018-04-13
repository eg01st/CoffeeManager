using System;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Shifts
{
    public partial class ExpenseItemCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("ExpenseItemCell");
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

        static ExpenseItemCell()
        {
            Nib = UINib.FromName("ExpenseItemCell", NSBundle.MainBundle);
        }

        protected ExpenseItemCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.

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
                var set = this.CreateBindingSet<ExpenseItemCell, ExpenseItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(AmountLabel).To(vm => vm.Amount);
                set.Bind(ItemCountLabel).To(vm => vm.ItemCount);
                set.Bind(this).For(t => t.DeleteCommand).To(vm => vm.DeleteExpenseCommand);
                set.Apply();
            });
        }

    }
}
