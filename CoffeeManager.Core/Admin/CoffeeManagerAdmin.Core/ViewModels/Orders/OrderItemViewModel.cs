using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Managers;
using CoffeeManagerAdmin.Core.Messages;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Orders
{
    public class OrderItemViewModel : ViewModelBase
    {
        private SuplyOrderManager _manager = new SuplyOrderManager();
        private OrderItem _item;

        private decimal _price;
        private int _quantity;
        private bool _isDone;
        private string _suplyProductName;

        private bool _isPromt;
        public OrderItemViewModel(OrderItem item)
        {
            _item = item;
            IsDone = item.IsDone;
            Price = item.Price;
            SuplyProductName = item.SuplyProductName;
            Quantity = (int)item.Quantity;
            SaveItemCommand = new MvxCommand(DoSaveItem);
            DeleteItemCommand = new MvxCommand(DoDeleteItem);
        }

        private void DoDeleteItem()
        {
            if (!_isPromt && !IsDone)
            {
                _isPromt = true;
                UserDialogs.Confirm(new ConfirmConfig
                {
                    Message = "Удалить заявку?",
                    OnAction = OnDelete
                });
            }

        }

        private async void OnDelete(bool ok)
        {
            if (ok)
            {
                await _manager.DeleteOrderItem(_item.Id);
                Publish(new OrderItemChangedMessage(this));
            }
            _isPromt = false;
        }

        private void DoSaveItem()
        {
            if (!_isPromt && !IsDone)
            {
                _isPromt = true;
                UserDialogs.Prompt(new PromptConfig
                {
                    Message = "Введите цену",
                    OnAction = OnAction,
                    InputType = InputType.DecimalNumber,
                    Text = Price.ToString("F")
                });

            }

        }

        private async void OnAction(PromptResult obj)
        {
            if (obj.Ok)
            {
                decimal price;
                if (decimal.TryParse(obj.Text, out price))
                {
                    IsDone = !IsDone;
                    await _manager.SaveOrderItem(new OrderItem
                    {
                        Id = _item.Id,
                        IsDone = IsDone,
                        Price = price,
                        Quantity = Quantity
                    });
                    Price = price;
                    Publish(new OrderItemChangedMessage(this));
                }
            }
            _isPromt = false;
        }

        public string SuplyProductName
        {
            get { return _suplyProductName; }
            set
            {
                _suplyProductName = value;
                RaisePropertyChanged(nameof(SuplyProductName));
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
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                RaisePropertyChanged(nameof(Quantity));
            }
        }

        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                _isDone = value;
                RaisePropertyChanged(nameof(IsDone));
            }
        }

        public ICommand SaveItemCommand { get; set; }

        public ICommand DeleteItemCommand { get; set; }
    }
}
