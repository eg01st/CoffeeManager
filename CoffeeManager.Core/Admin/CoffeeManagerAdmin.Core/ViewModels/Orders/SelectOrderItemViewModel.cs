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
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Orders
{
    public class SelectOrderItemViewModel : ListItemViewModelBase
    {
        private int _orderId;
        private SupliedProduct _prod;
        private int? _quantity;
        private bool _isSelected;

        private bool _isPromt;

        public SelectOrderItemViewModel(int orderId, SupliedProduct prod)
        {
            _prod = prod;
            _orderId = orderId;
            AddRequestItemCommand = new MvxCommand(DoAddRequestItem);
            DeleteItemCommand = new MvxCommand(DoDeleteItem);
        }

        private void DoDeleteItem()
        {
            if (!_isPromt)
            {
                _isPromt = true;
                UserDialogs.Confirm(new ConfirmConfig()
                {
                    Message = $"Действительно удалить товар {_prod.Name}?",
                    OnAction = OnDeleteItem
                });
            }
        }

        private async void OnDeleteItem(bool ok)
        {
            if (ok)
            {
                var suplyManager = new SuplyProductsManager();
                await suplyManager.DeleteSuplyProduct(_prod.Id);
                Publish(new SuplyProductDeletedMessage(this));
            }
            _isPromt = false;
        }

        protected override void DoGoToDetails()
        {
            DoAddRequestItem();
        }

        private void DoAddRequestItem()
        {
            if (!_isPromt)
            {
                _isPromt = true;
                UserDialogs.Prompt(new PromptConfig()
                {
                    InputType = InputType.Number,
                    Message = "Укажите количество",
                    OnAction = AddItem,
                });
            }
        }

        private async void AddItem(PromptResult obj)
        {
            if (obj.Ok)
            {
                var manager = new SuplyOrderManager();
                await manager.CreateOrderItem(new OrderItem
                {
                    CoffeeRoomNo = Config.CoffeeRoomNo,
                    IsDone = false,
                    OrderId = _orderId,
                    Price = Price,
                    Quantity = int.Parse(obj.Text),
                    SuplyProductId = _prod.Id
                });

                IsSelected = true;
                Quantity = int.Parse(obj.Text);
            }
            _isPromt = false;
        }

        public ICommand AddRequestItemCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }


        public string Name => _prod.Name;

        public decimal Price => _prod.Price;

        public int? Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                RaisePropertyChanged(nameof(Quantity));
            }
        }


        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                if (!_isSelected)
                {
                    Quantity = null;
                }
                RaisePropertyChanged(nameof(IsSelected));
            }
        }
    }
}
