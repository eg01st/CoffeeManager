using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Managers;
using CoffeeManagerAdmin.Core.Messages;
using CoffeeManagerAdmin.Core.Util;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Orders
{
    public class OrderViewModel : ListItemViewModelBase
    {
        private SuplyOrderManager _manager = new SuplyOrderManager();
        private Order _order;


        private bool _isPromt;
        public OrderViewModel(Order order)
        {
            _order = order;
            DeleteOrderCommand = new MvxCommand(DoDeleteOrder);
        }

        protected override void DoGoToDetails()
        {
            var id = ParameterTransmitter.PutParameter(_order);
            ShowViewModel<OrderItemsViewModel>(new { id = id });
        }


        private void DoDeleteOrder()
        {
            if (!_isPromt)
            {
                _isPromt = true;
                UserDialogs.Confirm(new ConfirmConfig
                {
                    Message = "Удалить заказ?",
                    OnAction = OnDeleteOrder
                });
            }
        }

        private async void OnDeleteOrder(bool ok)
        {
            if (ok)
            {
                await _manager.DeleteOrder(_order.Id);
                Publish(new OrderListChangedMessage(this));
            }
            _isPromt = false;
        }

        public string Price => _order.Price.ToString("F");

        public string Date => _order.Date.ToString("MM-dd");

        public bool IsDone => _order.IsDone;

        public string Status => _order.IsDone ? "Выполнен" : "В процессе";

        public string DisplayName => $"{Date} Цена: {Price} грн ";

        public int Id => _order.Id;

        public ICommand DeleteOrderCommand { get; set; }

    }
}
