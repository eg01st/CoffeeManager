using System;
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

        static CategoryTableViewCell()
        {
            Nib = UINib.FromName("CategoryTableViewCell", NSBundle.MainBundle);
        }

        protected CategoryTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<CategoryTableViewCell, CategoryItemViewModel>();
                set.Bind(CategoryLabel).To(vm => vm.Name);
                set.Apply();
            });
        }
    }
}
