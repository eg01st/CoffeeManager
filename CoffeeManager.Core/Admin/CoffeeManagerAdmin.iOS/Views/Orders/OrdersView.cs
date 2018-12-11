using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels.Orders;
using CoffeeManagerAdmin.iOS.TableSources;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MobileCore.iOS.ViewControllers;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class OrdersView : ViewControllerBase<OrdersViewModel>
    {
        public OrdersView() : base("OrdersView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Заявки";

            var source = new SimpleTableSource(OrdersTable, OrderViewCell.Key, OrderViewCell.Nib);
            OrdersTable.Source = source;

            var set = this.CreateBindingSet<OrdersView, OrdersViewModel>();
            set.Bind(CreateNewOrderButton).To(vm => vm.CreateOrderCommand);
            set.Bind(source).To(vm => vm.ItemsCollection);
            set.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);
            set.Apply();
        }
    }
}

