using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels;
using CoffeeManagerAdmin.Core.ViewModels.Calculation;
using CoffeeManagerAdmin.iOS.TableSources;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views
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

            Title = "Добавить продукт";

            var btn = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_add_circle_outline")
            };


            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked AddNewSuplyProductCommand"},
            });
        }
    }
}

