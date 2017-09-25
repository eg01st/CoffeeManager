using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManagerAdmin.Core.Messages;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Util;
using CoffeManager.Common;
using CoffeeManager.Common;

namespace CoffeeManagerAdmin.Core.ViewModels.Orders
{
    public class OrdersViewModel : ViewModelBase
    {
        private MvxSubscriptionToken _token;

        private List<OrderViewModel> _items = new List<OrderViewModel>();
        private readonly ISuplyOrderManager manager;

        public OrdersViewModel(ISuplyOrderManager manager)
        {
            this.manager = manager;
            _token = Subscribe<OrderListChangedMessage>(async (a) => await LoadData());
            CreateOrderCommand = new MvxCommand(DoCreateOrder);
        }

        private async void DoCreateOrder()
        {
            var order = new Order()
            {
                IsDone = false,
                CoffeeRoomNo = Config.CoffeeRoomNo
            };

            int orderId = await manager.CreateOrder(order);
            order.Id = orderId;
            await LoadData();
            var id = ParameterTransmitter.PutParameter(order);
            ShowViewModel<OrderItemsViewModel>(new { id = id });
        }

        public ICommand CreateOrderCommand { get; set; }

        public List<OrderViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        public async void Init()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            var items = await manager.GetOrders();
            Items = items.Select(s => new OrderViewModel(manager, s)).OrderByDescending(o => o.Id).ToList();
        }

        protected override void DoUnsubscribe()
        {
            Unsubscribe<OrderListChangedMessage>(_token);
        }
    }
}
