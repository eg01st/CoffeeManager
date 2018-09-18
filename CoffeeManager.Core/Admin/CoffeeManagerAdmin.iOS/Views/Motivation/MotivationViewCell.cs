using System;
using CoffeeManagerAdmin.Core.ViewModels.Motivation;
using Foundation;
using MobileCore.iOS;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Motivation
{
    public partial class MotivationViewCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString("MotivationViewCell");
        public static readonly UINib Nib;

        static MotivationViewCell()
        {
            Nib = UINib.FromName("MotivationViewCell", NSBundle.MainBundle);
        }

        protected MotivationViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected override void DoBind()
        {
            base.DoBind();

            var set = this.CreateBindingSet<MotivationViewCell, MotivationItemViewModel>();
            set.Bind(NameLabel).To(vm => vm.UserName);
            set.Bind(EntireScoreLabel).To(vm => vm.EntireScore);
            set.Bind(MoneyScoreLabel).To(vm => vm.MoneyScore);
            set.Bind(ShiftScoreLabel).To(vm => vm.ShiftScore);
            set.Apply();
        }
    }
}
