using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Messages;
using CoffeeManagerAdmin.Core.Util;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Orders
{
    public class OrderItemsViewModel : ViewModelBase, IMvxViewModel<Order>
    {
        private MvxSubscriptionToken _token;
        private MvxSubscriptionToken _itemsSelectedtoken;
        private Order _order;
        private decimal _price;
        private int? _expenseTypeId;
        private string _expenseTypeName;
        private Entity _selectedExpenseType;
        private bool _isDone;

        private List<OrderItemViewModel> _items = new List<OrderItemViewModel>();
        private List<Entity> _expenseItems = new List<Entity>();
        private readonly IPaymentManager paymentManager;
        private readonly ISuplyOrderManager suplyOrderManager;

        public OrderItemsViewModel(IPaymentManager paymentManager, ISuplyOrderManager suplyOrderManager)
        {
            this.suplyOrderManager = suplyOrderManager;
            this.paymentManager = paymentManager;
            _token = Subscribe<OrderItemChangedMessage>(async (a) => await LoadData());
            _itemsSelectedtoken = Subscribe<OrderItemsListChangedMessage>(async (s) => await LoadData());
            CloseOrderCommand = new MvxCommand(DoCloseOrder);
            AddOrderItemsCommand = new MvxAsyncCommand(DoAddOrderItems);
        }

        private async Task DoAddOrderItems()
        {
            if (!IsDone)
            {
               await NavigationService.Navigate<SelectOrderItemsViewModel, int>(_order.Id );
            }
        }

        private void DoCloseOrder()
        {
            if (!_expenseTypeId.HasValue || _expenseTypeId == 0)
            {
                Alert("Выберите тип траты");
                return;
            }

            if (Items.All(i => !i.IsDone))
            {
                Alert("Не выполнена ни одна покупка");
                return;
            }

            if (!IsDone)
            {
                Confirm("Закрыть заявку? Сумма заказа будет списана с кассы", CloseOrder);
            }

        }

        private async Task CloseOrder()
        {
            await suplyOrderManager.CloseOrder(new Order
            {
                Id = _order.Id,
                Price = Price,
                ExpenseTypeId = ExpenseTypeId
            });
            Publish(new OrderListChangedMessage(this));
            Publish(new UpdateCashAmountMessage(this));
            Close(this);
        }

        private void ReloadPrice()
        {
            Price = Items.Where(s => s.IsDone).Sum(orderItemViewModel => orderItemViewModel.Price * orderItemViewModel.Quantity);
        }

        public override async Task Initialize()
        {
            Price = _order.Price;
            IsDone = _order.IsDone;
            ExpenseTypeId = _order.ExpenseTypeId;
            if (_order.Id > 0)
            {
                await LoadData();
            }
            var types = await paymentManager.GetExpenseItems();
            ExpenseItems = types.Select(s => new Entity { Id = s.Id, Name = s.Name }).ToList();
            if (ExpenseTypeId > 0)
            {
                var item = ExpenseItems.First(i => i.Id == ExpenseTypeId);
                SelectedExpenseType = item;
            }
        }


        public List<Entity> ExpenseItems
        {
            get { return _expenseItems; }
            set
            {
                _expenseItems = value;
                RaisePropertyChanged(nameof(ExpenseItems));
            }
        }

        private async Task LoadData()
        {
            var items = await suplyOrderManager.GetOrderItems(_order.Id);
            Items = items.Select(s => new OrderItemViewModel(suplyOrderManager, s)).ToList();
            ReloadPrice();
        }

        public ICommand CloseOrderCommand { get; set; }

        public ICommand AddOrderItemsCommand { get; set; }

        public List<OrderItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                RaisePropertyChanged(nameof(Price));
            }
        }

        public int? ExpenseTypeId
        {
            get { return _expenseTypeId; }
            set
            {
                _expenseTypeId = value;
                RaisePropertyChanged(nameof(ExpenseTypeId));
            }
        }

        public string ExpenseTypeName
        {
            get { return _expenseTypeName; }
            set
            {
                _expenseTypeName = value;
                RaisePropertyChanged(nameof(ExpenseTypeName));
            }
        }

        public Entity SelectedExpenseType
        {
            get { return _selectedExpenseType; }
            set
            {
                if (_selectedExpenseType != value)
                {
                    _selectedExpenseType = value;
                    RaisePropertyChanged(nameof(SelectedExpenseType));
                    ExpenseTypeId = _selectedExpenseType.Id;
                    ExpenseTypeName = _selectedExpenseType.Name;
                }
            }
        }

        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                _isDone = value;
                RaisePropertyChanged(nameof(IsDone));
                RaisePropertyChanged(nameof(Status));
            }
        }

        public string Status => IsDone ? "Выполнен" : "В процессе";

        protected override void DoUnsubscribe()
        {
            Unsubscribe<OrderItemsListChangedMessage>(_token);
            Unsubscribe<OrderItemsListChangedMessage>(_itemsSelectedtoken);
        }

        public void Prepare(Order parameter)
        {
            _order = parameter;
        }
    }
}
