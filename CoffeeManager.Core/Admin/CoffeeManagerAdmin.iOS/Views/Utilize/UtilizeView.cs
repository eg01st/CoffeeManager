using System;

using UIKit;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.iOS.Views.Abstract;

namespace CoffeeManagerAdmin.iOS
{
    public partial class UtilizeView : ViewControllerBase<UtilizeViewModel>
    {
        public UtilizeView() : base("UtilizeView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Списания";
            var source = new SimpleTableSource(UtilizedTableView, UtilizeItemCell.Key, UtilizeItemCell.Nib, UtilizeItemHeader.Key, UtilizeItemHeader.Nib);
            UtilizedTableView.Source = source;
            UtilizedTableView.RegisterNibForHeaderFooterViewReuse(UtilizeItemHeader.Nib, UtilizeItemHeader.Key);
            var set = this.CreateBindingSet<UtilizeView, UtilizeViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }

    }
}

