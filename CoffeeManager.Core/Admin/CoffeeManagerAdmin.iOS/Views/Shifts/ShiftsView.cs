using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ShiftsView : ViewControllerBase<ShiftsViewModel>
    {
        public ShiftsView() : base("ShiftsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Смены";
            var source = new SimpleTableSource(TableView, ShiftInfoCell.Key, ShiftInfoCell.Nib);
            TableView.Source = source;
            var set = this.CreateBindingSet<ShiftsView, ShiftsViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }
    }
}

