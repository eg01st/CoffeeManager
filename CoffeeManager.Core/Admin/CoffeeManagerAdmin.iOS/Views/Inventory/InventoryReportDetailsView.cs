using System;

using UIKit;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MobileCore.iOS.ViewControllers;

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
            var source = new SimpleTableSource(ReportDetailsTableView,InventoryDetailsItemCell.Key, InventoryDetailsItemCell.Nib, InventoryDetailsItemHeader.Key, InventoryDetailsItemHeader.Nib);
            ReportDetailsTableView.Source = source;
            var set = this.CreateBindingSet<InventoryReportDetailsView, InventoryReportDetailsViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }

    }
}

