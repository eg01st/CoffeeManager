using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels.Orders;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class OrdersView : MvxViewController
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
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

