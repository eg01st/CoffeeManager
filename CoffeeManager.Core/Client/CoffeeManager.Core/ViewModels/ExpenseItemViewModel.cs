using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Core.Messages;
using CoffeeManager.Models;
using CoffeManager.Common;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class ExpenseItemViewModel : ViewModelBase
    {
        private Expense _item;
        private bool _isPromt;
        private readonly IPaymentManager manager;

        public ExpenseItemViewModel(IPaymentManager manager, Expense item)
        {
            this.manager = manager;
            _item = item;
            DeleteItemCommand = new MvxCommand(DoDeleteItem);
        }

        private void DoDeleteItem()
        {
            if (!_isPromt)
            {
                _isPromt = true;
                UserDialogs.Confirm(new ConfirmConfig()
                {
                    Message = "Удалить трату?",
                    OnAction = OnDelete
                });
            }
        }

        private async void OnDelete(bool ok)
        {
            if (ok)
            {
                await manager.DeleteExpenseItem(_item.Id);
                Publish(new ExpenseDeletedMessage(this));
            }
            _isPromt = false;
        }

        public string Name => _item.Name;

        public string Amount => _item.Amount.ToString("F1");

        public int Quantity => _item.ItemCount;

        public ICommand DeleteItemCommand { get; set; }
    }
}
