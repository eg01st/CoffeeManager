using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;
using CoffeeManager.Common;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Orders
{
    public class SelectOrderItemViewModel : ListItemViewModelBase
    {
        private int _orderId;
        private SupliedProduct _prod;
        private int? _quantity;
        private bool _isSelected;

        private bool _isPromt;
        private readonly ISuplyProductsManager manager;
        readonly ISuplyOrderManager orderManager;

        public SelectOrderItemViewModel(ISuplyProductsManager manager, ISuplyOrderManager orderManager, int orderId, SupliedProduct prod)
        {
            this.orderManager = orderManager;
            this.manager = manager;
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
                await manager.DeleteSuplyProduct(_prod.Id);
                Publish(new SuplyListChangedMessage(this));
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
                int quantity;
                if (int.TryParse(obj.Text, out quantity))
                {
                    await orderManager.CreateOrderItem(new OrderItem
                    {
                        CoffeeRoomNo = Config.CoffeeRoomNo,
                        IsDone = false,
                        OrderId = _orderId,
                        Price = Price,
                        Quantity = quantity,
                        SuplyProductId = _prod.Id
                    });

                    IsSelected = true;
                    Quantity = int.Parse(obj.Text);
                }
                else
                {
                    Alert("Неверно указано количество");
                }
            }
            _isPromt = false;
        }

        public ICommand AddRequestItemCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }


        public override string Name => _prod.Name;

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
