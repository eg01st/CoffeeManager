using System;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.ViewModels.CoffeeCounter;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Counter
{
    public partial class CounterTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("CounterTableViewCell");
        public static readonly UINib Nib;

        public ICommand ToggleIsActiveCommand { get; set; }

        static CounterTableViewCell()
        {
            Nib = UINib.FromName("CounterTableViewCell", NSBundle.MainBundle);
        }

        protected CounterTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(DoBind);
        }

        private void DoBind()
        {
            var set = this.CreateBindingSet<CounterTableViewCell, CoffeeCounterItemViewModel>();
            set.Bind(NameLabel).To(vm => vm.Name);
            set.Bind(IsActiveSwitch).For(s => s.On).To(vm => vm.IsActive);
            set.Bind(this).For(t => t.ToggleIsActiveCommand).To(vm => vm.ToggleIsActiveCommand);
            set.Apply();
            IsActiveSwitch.ValueChanged += (sender, e) =>
            {
                ToggleIsActiveCommand.Execute(null);
            };
        }
    }
}
