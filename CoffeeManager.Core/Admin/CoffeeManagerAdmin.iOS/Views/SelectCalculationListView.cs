
using UIKit;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;
using System.Collections.Generic;

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

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var btn = new UIBarButtonItem()
            {
                Title = "Добавить товар"
            };


            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked AddNewSuplyProductCommand"},

            });
        }
    }
}

