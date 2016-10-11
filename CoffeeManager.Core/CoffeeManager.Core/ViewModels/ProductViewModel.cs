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
        
        private Product product;
        private bool _isPoliceSale;
        private decimal _price;
        private bool _enabled = true;

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

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                
                RaisePropertyChanged(nameof(Enabled));
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

        public int CupType => product.CupType;

        public ICommand SelectItemCommand => _selectItemCommand;

       

        public ProductViewModel(Product product)
        {
            this.product = product;
            Price = product.Price;
            _selectItemCommand = new MvxAsyncCommand(DoSelectItem);
            
            token = Subscribe<IsPoliceSaleMessage>((arg) => IsPoliceSale = arg.Data);
            //RaiseAllPropertiesChanged();

        }


        private async Task DoSelectItem()
        {
            if (!Enabled)
            {
                return;
            }
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
            Enabled = false;
            await ProductManager.SaleProduct(Id, Price, IsPoliceSale);
            Publish(new AmoutChangedMessage(new Tuple<decimal, bool>(Price, true), this));
            ShowSuccessMessage($"Продан товар {Name} !");
            Enabled = true;
        }
    }
}
