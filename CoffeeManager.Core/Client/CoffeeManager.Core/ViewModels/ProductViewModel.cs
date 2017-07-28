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
        private bool _isSelected;
        private bool _isCreditCardSale;
        private MvxSubscriptionToken _creditCardSaletoken;

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

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;

                RaisePropertyChanged(nameof(IsSelected));
            }
        }


        public bool IsCreditCardSale
        {
            get { return _isCreditCardSale; }
            set
            {
                _isCreditCardSale = value;

                RaisePropertyChanged(nameof(IsCreditCardSale));
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
            
            token = Subscribe<IsPoliceSaleMessage>((arg) =>
            {
                if (!IsSelected)
                {
                    IsPoliceSale = arg.Data;
                }
            });

            _creditCardSaletoken = Subscribe<IsCreditCardSaleMessage>((arg) =>
            {
                if (!IsSelected)
                {
                    IsCreditCardSale = arg.Data;
                }
            });
        }

        public ProductViewModel Clone()
        {
            return (ProductViewModel)this.MemberwiseClone();
        }

        private async Task DoSelectItem()
        {
            if (!Enabled)
            {
                return;
            }

            if (IsPoliceSale && IsCreditCardSale)
            {
                UserDialogs.Confirm(new ConfirmConfig()
                {
                    Message = $"Продать товар {Name} полицейскому через терминал?",
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
            else if (IsPoliceSale)
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
            else if (IsCreditCardSale)
            {
                UserDialogs.Confirm(new ConfirmConfig()
                {
                    Message = $"Оплата товара {Name} через терминал?",
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
            Publish(new ProductSelectedMessage(this));
            Enabled = true;
        }
    }
}
