using System;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.ViewModels.Categories;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Categories
{
    public partial class CategoryTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("CategoryTableViewCell");
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

        static CategoryTableViewCell()
        {
            Nib = UINib.FromName("CategoryTableViewCell", NSBundle.MainBundle);
        }

        protected CategoryTableViewCell(IntPtr handle) : base(handle)
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
                var set = this.CreateBindingSet<CategoryTableViewCell, CategoryItemViewModel>();
                set.Bind(CategoryLabel).To(vm => vm.Name);
                set.Bind(this).For(t => t.DeleteCommand).To(vm => vm.DeleteCategoryCommand);
                set.Apply();
            });
        }
    }
}
