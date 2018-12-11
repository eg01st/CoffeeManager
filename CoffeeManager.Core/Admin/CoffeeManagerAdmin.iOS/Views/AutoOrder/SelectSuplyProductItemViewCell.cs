using System;

using Foundation;
using UIKit;
using MobileCore.iOS;
using CoffeeManagerAdmin.Core.ViewModels.AutoOrder;
using MvvmCross.Binding.BindingContext;
using System.Windows.Input;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SelectSuplyProductItemViewCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString("SelectSuplyProductItemViewCell");
        public static readonly UINib Nib;

        public ICommand ToggleSelectedCommand { get; set; }

        static SelectSuplyProductItemViewCell()
        {
            Nib = UINib.FromName("SelectSuplyProductItemViewCell", NSBundle.MainBundle);
        }

        protected SelectSuplyProductItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected override void DoBind()
        {
            base.DoBind();
            var set = this.CreateBindingSet<SelectSuplyProductItemViewCell, SelectSuplyProductItemViewModel>();
            set.Bind(NameLabel).To(vm => vm.Name);

            set.Bind(IsActiveSwitch).For(s => s.On).To(vm => vm.IsSelected);
            set.Bind(this).For(t => t.ToggleSelectedCommand).To(vm => vm.ToggleSelectedCommand);
            set.Apply();
            IsActiveSwitch.ValueChanged += (sender, e) =>
            {
                ToggleSelectedCommand.Execute(null);
            };
        }
    }
}
