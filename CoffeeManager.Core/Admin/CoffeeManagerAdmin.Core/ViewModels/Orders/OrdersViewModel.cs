using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManagerAdmin.Core.Messages;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using CoffeeManager.Models;
using CoffeManager.Common;
using CoffeeManager.Common;
using CoffeManager.Common.ViewModels;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Orders
{
    public class OrdersViewModel : FeedViewModel<OrderViewModel>
    {
        private MvxSubscriptionToken _token;

        private List<OrderViewModel> _items = new List<OrderViewModel>();
        private readonly ISuplyOrderManager manager;

        public OrdersViewModel(ISuplyOrderManager manager)
        {
            this.manager = manager;
            _token = MvxMessenger.Subscribe<OrderListChangedMessage>(async (a) => await LoadData());
            CreateOrderCommand = new MvxAsyncCommand(DoCreateOrder);
        }

        private async Task DoCreateOrder()
        {
            var order = new Order()
            {
                IsDone = false,
                CoffeeRoomNo = Config.CoffeeRoomNo
            };

            int orderId = await manager.CreateOrder(order);
            order.Id = orderId;
            await LoadData();

            await NavigationService.Navigate<OrderItemsViewModel, Order>(order);
        }

        public ICommand CreateOrderCommand { get; set; }

        public override async Task Initialize()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            var items = await ExecuteSafe(manager.GetOrders);
            ItemsCollection.ReplaceWith(items.Select(s => new OrderViewModel(manager, s)).OrderByDescending(o => o.Id));
        }

        protected override void DoUnsubscribe()
        {
            MvxMessenger.Unsubscribe<OrderListChangedMessage>(_token);
        }
    }
}
