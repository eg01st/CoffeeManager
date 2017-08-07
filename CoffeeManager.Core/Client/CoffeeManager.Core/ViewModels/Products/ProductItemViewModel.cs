using System;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeManager.Common;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class ProductItemViewModel : ViewModelBase
    {
        private ICommand _selectItemCommand;
        
        private Product product;
        private bool _isPoliceSale;
        private decimal _price;
        private bool _isCreditCardSale;

        public event EventHandler<SaleItemEventArgs> ProductSelected;
        private Action saleAction;

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

        public bool IsSaleByWeight => product.IsSaleByWeight;

        public ICommand SelectItemCommand => _selectItemCommand;

        public ProductItemViewModel(Product product)
        {
            this.product = product;
            Price = product.Price;
            saleAction = () => Sale();
            _selectItemCommand = new MvxCommand(DoSelectItem);
        }


        private void DoSelectItem()
        {
            if (IsPoliceSale && IsCreditCardSale)
            {
                Confirm($"Продать товар {Name} полицейскому через терминал?", saleAction);
            }
            else if (IsPoliceSale)
            {
                Confirm($"Продать товар {Name} полицейскому?", saleAction);
            }
            else if (IsCreditCardSale)
            {
                Confirm($"Оплата товара {Name} через терминал?", saleAction);
            }
            else
            {
                saleAction();
            }
        }

        private async void Sale()
        {
            decimal? weight = null;
            decimal price = Price;
            if(IsSaleByWeight)
            {
                weight = await PromtAsync("Введите вес в граммах:");
                if(!weight.HasValue)
                {
                    return;
                }
                price = (int)(price * weight.Value / 100);
            }

            var handler = ProductSelected;
            if(handler != null)
            {
                handler.Invoke(this, new SaleItemEventArgs(price, weight, IsSaleByWeight));
            }
        }
    }
}
