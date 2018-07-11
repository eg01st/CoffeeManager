using System;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.Product;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class ProductItemViewModel : ViewModelBase
    {
        private ICommand _selectItemCommand;
        
        private ProductDTO productDTO;
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
                Price = IsPoliceSale ? productDTO.PolicePrice : productDTO.Price;
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

        public int Id => productDTO.Id;

        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                RaisePropertyChanged(nameof(Price));
            }
        }

        public string Name => productDTO.Name;

        public int CupType => productDTO.CupType;

        public bool IsSaleByWeight => productDTO.IsSaleByWeight;

        public ICommand SelectItemCommand => _selectItemCommand;

        public ProductItemViewModel(ProductDTO productDTO)
        {
            this.productDTO = productDTO;
            Price = productDTO.Price;
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
