using CoffeeManagerAdmin.Core.ViewModels.Shifts;
using CoffeeManagerAdmin.iOS.TableSources;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Shifts.Counters
{
    public partial class ShiftCountersView : ViewControllerBase<ShiftCountersViewModel>
    {
        private SimpleTableSource source;
        
        public ShiftCountersView() : base("ShiftCountersView", null)
        {
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);
            Title = "Счетчики кофемолок";
        }

        protected override void DoViewDidLoad()
        {
            source = new SimpleTableSource(
                CountersTableView,
                ShiftCounterTableViewCell.Key,
                ShiftCounterTableViewCell.Nib,
                ShiftCounterTableViewHeader.Key,
                ShiftCounterTableViewHeader.Nib);
            CountersTableView.Source = source;
        }

        protected override void DoBind()
        {
            base.DoBind();
            var set = this.CreateBindingSet<ShiftCountersView, ShiftCountersViewModel>();
            set.Bind(source).To(vm => vm.ItemsCollection);
            set.Apply();
        }
    }
}

