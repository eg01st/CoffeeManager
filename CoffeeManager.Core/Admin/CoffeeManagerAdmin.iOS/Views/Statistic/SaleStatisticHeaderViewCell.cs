using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.iOS.Converters;
using MobileCore.ViewModels.Items;
using MvvmCross.Binding.iOS;
using MobileCore.iOS;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SaleStatisticHeaderViewCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString("SaleStatisticHeaderViewCell");
        public static readonly UINib Nib;

        static SaleStatisticHeaderViewCell()
        {
            Nib = UINib.FromName("SaleStatisticHeaderViewCell", NSBundle.MainBundle);
        }

        protected SaleStatisticHeaderViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected override void Stylize()
        {
            base.Stylize();
            SelectionStyle = UITableViewCellSelectionStyle.None;
        }

        protected override void DoBind()
        {
            base.DoBind();
            var set = this.CreateBindingSet<SaleStatisticHeaderViewCell, SectionHeaderItemViewModel>();
            set.Bind(NameLabel).To(vm => vm.Title);
            set.Bind(AmountLabel).To(vm => vm.RighTitle);
            set.Bind(CollapseImageView).For(i => i.BindVisible()).To(vm => vm.IsExpandable);
            set.Bind(CollapseImageView).For(i => i.Image).To(vm => vm.IsExpanded).WithConversion(ExpandedImageConverter.Instance);
            set.Apply();
        }
    }
}
