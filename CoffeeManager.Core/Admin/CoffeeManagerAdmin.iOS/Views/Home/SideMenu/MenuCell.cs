using System;
using CoffeeManagerAdmin.Core.ViewModels.Home;
using Foundation;
using MobileCore.iOS;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Home.SideMenu
{
    public partial class MenuCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString("MenuCell");
        public static readonly UINib Nib;

        static MenuCell()
        {
            Nib = UINib.FromName("MenuCell", NSBundle.MainBundle);
        }

        protected MenuCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.SelectionStyle = UITableViewCellSelectionStyle.None;
        }

        protected override void DoBind()
        {
            var bindingSet = this.CreateBindingSet<MenuCell, MenuFeedtemViewModel>();
            bindingSet.Bind(TitleLabel).To(vm => vm.Title);
            bindingSet.Bind(this).For(BindingConstants.Selected).To(vm => vm.IsSelected);
            bindingSet.Apply();
        }
    }
}
