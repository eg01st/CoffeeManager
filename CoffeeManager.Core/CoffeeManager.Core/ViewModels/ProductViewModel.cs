using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Core.Messages;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private ICommand _selectItemCommand;
        private ICommand _dismisItemCommand;
        private Product product;

        public int Id => product.Id;

        public float Price => product.Price;

        public string Name => product.Name;

        public ICommand SelectItemCommand => _selectItemCommand;

        public ICommand DismisItemCommand => _dismisItemCommand;

        public ProductViewModel(Product product)
        {
            this.product = product;
            _selectItemCommand = new MvxAsyncCommand(DoSelectItem);
            _dismisItemCommand = new MvxAsyncCommand(DoDismisItem);
        }

        private Task DoDismisItem()
        {
            return Task.Run(() =>
            {
                UserDialogs.Confirm(new ConfirmConfig()
                {
                    Message = $"Отменить продажу товара {Name} ?",
                    OnAction =
                        (confirm) =>
                        {
                            if (confirm)
                            {
                                ProductManager.DismisSaleProduct(Id);
                                Publish(new AmoutChangedMessage(new Tuple<float, bool>(Price, false), this));
                                ShowSuccessMessage($"Отменена продажа товара {Name} !");
                            }
                        }
                });
            });
        }

        private Task DoSelectItem()
        {
            return Task.Run(() =>
            {
                ProductManager.SaleProduct(Id);
                Publish(new AmoutChangedMessage(new Tuple<float, bool>(Price, true), this));
                ShowSuccessMessage($"Продан товар {Name} !");
            });
        }
    }
}
