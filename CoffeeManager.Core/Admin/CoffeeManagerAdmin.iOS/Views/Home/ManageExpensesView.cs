using System;
using System.Collections.Generic;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.Core.ViewModels.ManageExpenses;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views.Presenters.Attributes;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Расходы",
                        TabIconName = "ic_arrow_upward.png",
                        TabSelectedIconName = "ic_arrow_upward.png")]
    public partial class ManageExpensesView : SearchViewController<ManageExpensesView, ManageExpensesViewModel, ManageExpenseItemViewModel>
    {
        protected override SimpleTableSource TableSource => new SimpleTableSource(TableView, ManageExpensesTableViewCell.Key, ManageExpensesTableViewCell.Nib);

        protected override UIView TableViewContainer => this.ContainerView;

        protected override MvxFluentBindingDescriptionSet<ManageExpensesView, ManageExpensesViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<ManageExpensesView, ManageExpensesViewModel>();
        }

        protected override bool UseCustomBackButton => false;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Расходы";

            var btn = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_add_circle_outline")
            };


            NavigationItem.SetRightBarButtonItem(btn, true);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked AddExpenseTypeCommand"},

            });
        }

    }
}

