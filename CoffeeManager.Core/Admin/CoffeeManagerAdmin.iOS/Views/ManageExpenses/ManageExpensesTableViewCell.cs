using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using System.Windows.Input;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ManageExpensesTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("ManageExpensesTableViewCell");
        public static readonly UINib Nib;

        static ManageExpensesTableViewCell()
        {
            Nib = UINib.FromName("ManageExpensesTableViewCell", NSBundle.MainBundle);
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

        private ICommand deleteExpenseTypeCommand;
        public ICommand DeleteExpenseTypeCommand
        {
            get { return deleteExpenseTypeCommand; }
            set
            {
                deleteExpenseTypeCommand = value;

            }
        }

        protected ManageExpensesTableViewCell(IntPtr handle) : base(handle)
        {
            var longPressGesture = new UILongPressGestureRecognizer((sender) =>
            {
                if(sender.State == UIGestureRecognizerState.Began)
                {
                    DeleteExpenseTypeCommand?.Execute(null);
                }
            });
            AddGestureRecognizer(longPressGesture);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ManageExpensesTableViewCell, ManageExpenseItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(IsActiveSwitch).For(s => s.On).To(vm => vm.IsActive);
                set.Bind(this).For(t => t.ToggleIsActiveCommand).To(vm => vm.ToggleIsActiveCommand);
                set.Bind(this).For(t => t.DeleteExpenseTypeCommand).To(vm => vm.DeleteExpenseTypeCommand);
                set.Apply();    
                IsActiveSwitch.ValueChanged += (sender, e) => 
                {
                    ToggleIsActiveCommand.Execute(null);
                };  
            }
            );
        }
    }
}
