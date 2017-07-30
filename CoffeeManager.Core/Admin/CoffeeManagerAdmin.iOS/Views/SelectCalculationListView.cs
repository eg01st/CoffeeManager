
using UIKit;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SelectCalculationListView : SearchViewController<SelectCalculationListView, SelectCalculationListViewModel, SelectCalculationItemViewModel>
    {
        protected override SimpleTableSource TableSource => new SimpleTableSource(TableView, SelectCalculationItemViewCell.Key, SelectCalculationItemViewCell.Nib);

        protected override UIView TableViewContainer => ContainerView;

        public SelectCalculationListView() : base("SelectCalculationListView", null)
        {
        }

        protected override MvxFluentBindingDescriptionSet<SelectCalculationListView, SelectCalculationListViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<SelectCalculationListView, SelectCalculationListViewModel>();
        }
    }
}

