using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Managers;
using CoffeeManagerAdmin.Core.Messages;
using CoffeeManagerAdmin.Core.Util;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.Orders
{
    public class OrderItemsViewModel : ViewModelBase
    {
        private SuplyOrderManager _manager = new SuplyOrderManager();
        private PaymentManager _paymentManager = new PaymentManager();
        private MvxSubscriptionToken _token;
        private MvxSubscriptionToken _itemsSelectedtoken;

        private bool _isPromt;
        private Order _order;
        private decimal _price;
        private int? _expenseTypeId;
        private string _expenseTypeName;
        private Entity _selectedExpenseType;
        private bool _isDone;

        private List<OrderItemViewModel> _items = new List<OrderItemViewModel>();
        private List<Entity> _expenseItems = new List<Entity>();


        public OrderItemsViewModel()
        {
            _token = Subscribe<OrderItemChangedMessage>(async (a) => await LoadData());
            _itemsSelectedtoken = Subscribe<OrderItemsListChangedMessage>(async (s) => await LoadData());
            CloseOrderCommand = new MvxCommand(DoCloseOrder);
            AddOrderItemsCommand = new MvxCommand(DoAddOrderItems);
        }

        private void DoAddOrderItems()
        {
            if (!IsDone)
            {
                ShowViewModel<SelectOrderItemsViewModel>(new { id = _order.Id });
            }
        }

        private void DoCloseOrder()
        {
            if (!_expenseTypeId.HasValue || _expenseTypeId == 0)
            {
                UserDialogs.Alert("Выберите тип траты");
                return;
            }

            if (Items.All(i => !i.IsDone))
            {
                UserDialogs.Alert("Не выполнена ни одна покупка");
                return;
            }

            if (!_isPromt && !IsDone)
            {
                _isPromt = true;
                UserDialogs.Confirm(new ConfirmConfig
                {
                    Message = "Закрыть заявку? Сумма заказа будет списана с кассы",
                    OnAction = OnCloseOrder
                });
            }

        }

        private async void OnCloseOrder(bool ok)
        {
            if (ok)
            {
                await _manager.CloseOrder(new Order
                {
                    Id = _order.Id,
                    Price = Price,
                    ExpenseTypeId = ExpenseTypeId
                });
                Publish(new OrderListChangedMessage(this));
                Close(this);
            }
            _isPromt = false;
        }

        private void ReloadPrice()
        {
            Price = Items.Where(s => s.IsDone).Sum(orderItemViewModel => orderItemViewModel.Price * orderItemViewModel.Quantity);
        }

        public async void Init(Guid id)
        {
            ParameterTransmitter.TryGetParameter<Order>(id, out _order);
            Price = _order.Price;
            IsDone = _order.IsDone;
            ExpenseTypeId = _order.ExpenseTypeId;
            if (_order.Id > 0)
            {
                await LoadData();
            }
            var types = await _paymentManager.GetExpenseItems();
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
            var items = await _manager.GetOrderItems(_order.Id);
            Items = items.Select(s => new OrderItemViewModel(s)).ToList();
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
    }
}
