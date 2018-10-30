using System;

using UIKit;
using MobileCore.iOS.ViewControllers;
using CoffeeManagerAdmin.Core.ViewModels.AutoOrder;
using CoffeeManagerAdmin.iOS.TableSources;
using MvvmCross.Binding.BindingContext;
using System.Collections.Generic;

namespace CoffeeManagerAdmin.iOS
{
    public partial class AutoOrderView : ViewControllerBase<AutoOrderViewModel>
    {
        private SimpleTableSource tableSource;

        public AutoOrderView() : base("AutoOrderView", null)
        {
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            Title = "Автозаказ";

            var addProductButtonItem = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_add_circle_outline")
            };

            NavigationItem.SetRightBarButtonItem(addProductButtonItem, true);
            this.AddBindings(new Dictionary<object, string>
            {
                {addProductButtonItem, "Clicked AddNewAutoOrderCommad"},
            });

            tableSource = new SimpleTableSource(OrdersTableView, AutoOrderItemViewCell.Key, AutoOrderItemViewCell.Nib);
            OrdersTableView.Source = tableSource;

        }
        protected override void DoBind()
        {
            base.DoBind();
            var set = this.CreateBindingSet<AutoOrderView, AutoOrderViewModel>();
            set.Bind(tableSource).For(p => p.ItemsSource).To(vm => vm.ItemsCollection);
            set.Bind(tableSource).For(p => p.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);
            set.Apply();
        }
    }
}

