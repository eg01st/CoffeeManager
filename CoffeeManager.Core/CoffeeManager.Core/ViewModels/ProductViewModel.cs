using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Core.Messages;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManager.Core.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private readonly MvxSubscriptionToken token;
        private ICommand _selectItemCommand;
        private ICommand _dismisItemCommand;
        private Product product;
        private bool _isPoliceSale;
        private decimal _price;

        public bool IsPoliceSale
        {
            get { return _isPoliceSale; }
            set
            {
                _isPoliceSale = value;
                Price = IsPoliceSale ? product.PolicePrice : product.Price;
                RaisePropertyChanged(nameof(IsPoliceSale));
            }
        }

        public int Id => product.Id;

        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                RaisePropertyChanged(nameof(Price));
            }
        }

        public string Name => product.Name;

        public ICommand SelectItemCommand => _selectItemCommand;

        public ICommand DismisItemCommand => _dismisItemCommand;

        public ProductViewModel(Product product)
        {
            this.product = product;
            Price = product.Price;
            _selectItemCommand = new MvxAsyncCommand(DoSelectItem);
            _dismisItemCommand = new MvxAsyncCommand(DoDismisItem);
            token = Subscribe<IsPoliceSaleMessage>((arg) => IsPoliceSale = arg.Data);
            //RaiseAllPropertiesChanged();

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
                                Publish(new AmoutChangedMessage(new Tuple<decimal, bool>(Price, false), this));
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
            await ProductManager.SaleProduct(Id, Price, IsPoliceSale);
            Publish(new AmoutChangedMessage(new Tuple<decimal, bool>(Price, true), this));
            ShowSuccessMessage($"Продан товар {Name} !");
        }
    }
}
