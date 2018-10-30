using System;

using Foundation;
using UIKit;
using MobileCore.iOS;
using CoffeeManagerAdmin.Core.ViewModels.AutoOrder;
using MvvmCross.Binding.BindingContext;
using System.Windows.Input;

namespace CoffeeManagerAdmin.iOS
{
    public partial class AutoOrderItemViewCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString("AutoOrderItemViewCell");
        public static readonly UINib Nib;

        public ICommand ToggleOrderEnabledCommand { get; set; }

        static AutoOrderItemViewCell()
        {
            Nib = UINib.FromName("AutoOrderItemViewCell", NSBundle.MainBundle);
        }

        protected AutoOrderItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected override void DoBind()
        {
            base.DoBind();
            var set = this.CreateBindingSet<AutoOrderItemViewCell, AutoOrderItemViewModel>();
            set.Bind(DayOfWeekLabel).To(vm => vm.DayOfWeek);
            set.Bind(HourLabel).To(vm => vm.OrderTime);

            set.Bind(IsActiveSwitch).For(s => s.On).To(vm => vm.IsActive);
            set.Bind(this).For(t => t.ToggleOrderEnabledCommand).To(vm => vm.ToggleOrderEnabledCommand);
            set.Apply();
            IsActiveSwitch.ValueChanged += (sender, e) =>
            {
                ToggleOrderEnabledCommand.Execute(null);
            };
        }
    }
}
