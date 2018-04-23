using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.Core.ViewModels.Settings;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ClientViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("ClientViewCell");
        public static readonly UINib Nib;

        static ClientViewCell()
        {
            Nib = UINib.FromName("ClientViewCell", NSBundle.MainBundle);
        }

        protected ClientViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ClientViewCell, ClientItemViewModel>();
                set.Bind(ClientNameLabel).To(vm => vm.Name);
                set.Apply();
            });
        }
    }
}
