using System;
using CoffeeManagerAdmin.Core.ViewModels.Home;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Home
{
    public partial class MoreItemViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("MoreItemViewCell");
        public static readonly UINib Nib;

        static MoreItemViewCell()
        {
            Nib = UINib.FromName("MoreItemViewCell", NSBundle.MainBundle);
        }

        protected MoreItemViewCell(IntPtr handle) : base(handle)
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
            var set = this.CreateBindingSet<MoreItemViewCell, MenuFeedtemViewModel>();
            set.Bind(TitleLabel).To(vm => vm.Title);
            set.Apply();
        }

    }
}
