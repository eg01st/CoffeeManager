using System;
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
        private bool _isPoliceSale;

        public bool IsPoliceSale => product.IsPoliceSale;

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
                         async
                         (confirm) =>
                        {
                            if (confirm)
                            {
                                await ProductManager.DismisSaleProduct(Id);
                                Publish(new AmoutChangedMessage(new Tuple<float, bool>(Price, false), this));
                                ShowSuccessMessage($"Отменена продажа товара {Name} !");
                            }
                        }
                });
            });
        }

        private async Task DoSelectItem()
        {
            if (IsPoliceSale)
            {
                UserDialogs.Confirm(new ConfirmConfig()
                {
                    Message = $"Продать товар {Name} полицейскому?",
                    OnAction = async 
                        (confirm) =>
                        {
                            if (confirm)
                            {
                                await Sale();
                            }
                        }
                });
            }
            else
            {
                await Sale();
            }
        }

        private async Task Sale()
        {
            await ProductManager.SaleProduct(Id, IsPoliceSale);
            Publish(new AmoutChangedMessage(new Tuple<float, bool>(Price, true), this));
            ShowSuccessMessage($"Продан товар {Name} !");
        }
    }
}
