using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Messages;
using CoffeeManagerAdmin.Core.Util;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Orders
{
    public class OrderViewModel : ListItemViewModelBase
    {
        private Order _order;

        private bool _isPromt;
        private readonly ISuplyOrderManager manager;

        public OrderViewModel(ISuplyOrderManager manager, Order order)
        {
            this.manager = manager;
            _order = order;
            DeleteOrderCommand = new MvxCommand(DoDeleteOrder);
        }

        protected override async void DoGoToDetails()
        {
            await NavigationService.Navigate<OrderItemsViewModel, Order>(_order);
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
                await manager.DeleteOrder(_order.Id);
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
