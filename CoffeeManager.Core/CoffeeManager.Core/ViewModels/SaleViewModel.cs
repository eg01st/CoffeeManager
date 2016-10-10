using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Core.Messages;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class SaleViewModel : ViewModelBase
    {
        private ICommand _dismisItemCommand;
        private Sale _sale;
        public SaleViewModel(Sale sale)
        {
            _sale = sale;
            _dismisItemCommand = new MvxAsyncCommand(DoDismisItem);
        }

        public ICommand DismisItemCommand => _dismisItemCommand;
        public string Name => _sale.Product1.Name;

        public decimal Amount => _sale.Amount;

        public bool IsPoliceSale => _sale.IsPoliceSale;

        public string Time => _sale.Time.ToString();

        private Task DoDismisItem()
        {
            return Task.Run(() =>
            {
                UserDialogs.Confirm(new ConfirmConfig()
                {
                    Message = $"Отменить продажу товара {Name} ?",
                    OnAction =
                         async
                         (confirm) =>
                         {
                             if (confirm)
                             {
                                 await ProductManager.DismisSaleProduct(_sale.Id);
                                 Publish(new AmoutChangedMessage(new Tuple<decimal, bool>(_sale.Amount, false), this));
                                 Publish(new SaleRemovedMessage(this));
                                 ShowSuccessMessage($"Отменена продажа товара {Name} !");
                             }
                         }
                });
            });
        }

    }
}
