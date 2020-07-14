using System;

using Foundation;
using MobileCore.iOS.Common;
using UIKit;

namespace MobileCore.iOS
{
    public partial class PageTitleViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("PageTitleViewCell");
        public static readonly UINib Nib;

        private bool isSelected;

        static PageTitleViewCell()
        {
            Nib = UINib.FromName("PageTitleViewCell", NSBundle.MainBundle);
        }


        public static PageTitleViewCell Create()
        {
            return (PageTitleViewCell)Nib.Instantiate(null, null)[0];
        }

        protected PageTitleViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override bool Selected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                SwitchSelectedState(value);
            }
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            ContentView.TranslatesAutoresizingMaskIntoConstraints = false;
        }

        public void SetTitle(string title)
        {
            PageTitleLabel.Text = title;
        }

        private void SwitchSelectedState(bool selected)
        {
            IndicatorView.Hidden = !selected;
            PageTitleLabel.TextColor = selected ? Colors.Black : Colors.GreyChateau;
        }
    }
}
