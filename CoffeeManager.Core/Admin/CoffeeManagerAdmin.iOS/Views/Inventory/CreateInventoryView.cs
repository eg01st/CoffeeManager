using System;
using System.Collections.Generic;
using CoffeeManagerAdmin.Core;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class CreateInventoryView : SearchViewController<CreateInventoryView, CreateInventoryViewModel, CreateInventoryItemViewModel>
    {
        protected override SimpleTableSource TableSource => new CreateInventoryTableSource(TableView, CreateInventoryCell.Key, CreateInventoryCell.Nib);

        protected override UIView TableViewContainer => Container;

        public CreateInventoryView() : base("CreateInventoryView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Переучет";

            TableView.RowHeight = UITableView.AutomaticDimension;
            TableView.EstimatedRowHeight = 100f;

            var btn = new UIBarButtonItem()
            {
                Title = "Готово"
                //Image = UIImage.FromBundle("ic_add_circle_outline")
            };


            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked SendReportCommand"},
            });
        }

        protected override MvxFluentBindingDescriptionSet<CreateInventoryView, CreateInventoryViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<CreateInventoryView, CreateInventoryViewModel>();
        }
    }
}

