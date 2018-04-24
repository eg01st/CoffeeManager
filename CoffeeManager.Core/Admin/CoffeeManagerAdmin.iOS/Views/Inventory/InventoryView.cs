using System;

using UIKit;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.Inventory;
using CoffeeManagerAdmin.iOS.TableSources;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MobileCore.iOS.ViewControllers;

namespace CoffeeManagerAdmin.iOS
{
    public partial class InventoryView : ViewControllerBase<InventoryViewModel>
    {
        public InventoryView() : base("InventoryView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var addButton = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_add_circle_outline")
            };

            this.AddBindings(new Dictionary<object, string>
            {
                {addButton, "Clicked CreateReportCommand"},
            });

            NavigationItem.SetRightBarButtonItems(new[] { addButton }, true);


            Title = "Отчеты переучетов";
            var source = new SimpleTableSource(ReportsTableView, InventoryReportCell.Key, InventoryReportCell.Nib);
            ReportsTableView.Source = source;

            var set = this.CreateBindingSet<InventoryView, InventoryViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }
    }
}

