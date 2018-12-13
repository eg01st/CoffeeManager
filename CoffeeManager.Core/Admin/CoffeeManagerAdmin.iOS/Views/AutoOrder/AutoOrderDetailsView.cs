using System;

using UIKit;
using MobileCore.iOS.ViewControllers;
using CoffeeManagerAdmin.Core.ViewModels.AutoOrder;
using CoffeeManagerAdmin.iOS.TableSources;
using System.Collections.Generic;
using CoffeeManagerAdmin.iOS.Views.AutoOrder;
using MvvmCross.Binding.BindingContext;

namespace CoffeeManagerAdmin.iOS
{
    public partial class AutoOrderDetailsView : ViewControllerBase<AutoOrderDetailsViewModel>
    {
        private SimpleTableSource tableSource;

        public AutoOrderDetailsView() : base("AutoOrderDetailsView", null)
        {
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            tableSource = new SimpleTableSource(OrderItemsTableView, SuplyProductToOrderItemViewCell.Key, SuplyProductToOrderItemViewCell.Nib);
            OrderItemsTableView.Source = tableSource;

            Title = "Детали автозаказа";

            var btn = new UIBarButtonItem()
            {
                Title = "Удалить"
            };
            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked DeleteOrderCommand"},
            });
        }

        protected override void DoBind()
        {
            base.DoBind();
            var set = this.CreateBindingSet<AutoOrderDetailsView, AutoOrderDetailsViewModel>();
            set.Bind(OrderWeekDay).To(vm => vm.DayOfWeek);
            set.Bind(OrderTime).To(vm => vm.OrderTime);

            set.Bind(tableSource).For(p => p.ItemsSource).To(vm => vm.ItemsCollection);
            set.Bind(tableSource).For(p => p.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);

            set.Apply();
        }
    }
}

