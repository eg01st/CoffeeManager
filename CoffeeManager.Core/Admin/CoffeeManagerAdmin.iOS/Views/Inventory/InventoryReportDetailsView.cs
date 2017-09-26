using System;

using UIKit;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class InventoryReportDetailsView : ViewControllerBase<InventoryReportDetailsViewModel>
    {
        public InventoryReportDetailsView() : base("InventoryReportDetailsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Детали переучета";
            var source = new SimpleTableSourceWithHeader(ReportDetailsTableView,InventoryDetailsItemCell.Key, InventoryDetailsItemCell.Nib, InventoryDetailsItemHeader.Key);
            ReportDetailsTableView.Source = source;
            ReportDetailsTableView.RegisterNibForHeaderFooterViewReuse(InventoryDetailsItemHeader.Nib, InventoryDetailsItemHeader.Key);
            var set = this.CreateBindingSet<InventoryReportDetailsView, InventoryReportDetailsViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }

    }
}

