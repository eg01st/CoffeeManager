using System;
using CoffeeManagerAdmin.Core.ViewModels.Shifts;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Shifts.Counters
{
    public partial class ShiftCounterTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("ShiftCounterTableViewCell");
        public static readonly UINib Nib;

        static ShiftCounterTableViewCell()
        {
            Nib = UINib.FromName("ShiftCounterTableViewCell", NSBundle.MainBundle);
        }

        protected ShiftCounterTableViewCell(IntPtr handle) : base(handle)
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
            var set = this.CreateBindingSet<ShiftCounterTableViewCell, ShiftCounterItemViewModel>();
            set.Bind(NameLabel).To(vm => vm.SuplyProductName);
            set.Bind(StartLabel).To(vm => vm.StartCounter);
            set.Bind(FinishLabel).To(vm => vm.EndCounter);
            set.Bind(DiffLabel).To(vm => vm.Diff);
            set.Apply();
        }
    }
}
