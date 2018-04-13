using System;
using Foundation;
using MobileCore.iOS;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Home.SideMenu
{
    public partial class MenuHeaderCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString("MenuHeaderCell");
        public static readonly UINib Nib;

        static MenuHeaderCell()
        {
            Nib = UINib.FromName("MenuHeaderCell", NSBundle.MainBundle);
        }

        protected MenuHeaderCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            SelectionStyle = UITableViewCellSelectionStyle.None;
        }
    }
}
