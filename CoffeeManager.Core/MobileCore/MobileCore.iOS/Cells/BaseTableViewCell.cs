using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;

namespace MobileCore.iOS
{
    public abstract class BaseTableViewCell : MvxTableViewCell
    {
        protected BaseTableViewCell(IntPtr handle) : base(handle)
        {
        }

        //
        // Methods
        //
        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.Stylize();
            MvxBindingContextOwnerExtensions.DelayBind(this, new Action(this.DoBind));
        }

        protected virtual void DoBind()
        {
        }

        protected virtual void Stylize()
        {
        }
    }
}
