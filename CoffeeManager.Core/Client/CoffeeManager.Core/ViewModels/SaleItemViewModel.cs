using System;
using System.Collections.Generic;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Core.Messages;
using CoffeeManager.Models;
using CoffeManager.Common;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class SaleItemViewModel : ViewModelBase
    {
        private readonly IProductManager productManager;
        private ICommand _dismisItemCommand;
        private Sale _sale;
        private string _status;

        public SaleItemViewModel(IProductManager productManager, Sale sale)
        {
            this.productManager = productManager;
            _sale = sale;
            _dismisItemCommand = new MvxCommand(DoDismisItem);
            if (_sale.IsRejected)
            {
                Status = "Отменена";
            }
            if (_sale.IsUtilized)
            {
                Status = "Списана";
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                RaisePropertyChanged(nameof(Status));
            }
        }

        public ICommand DismisItemCommand => _dismisItemCommand;
        public string Name => _sale.ProductName;

        public decimal Amount => _sale.Amount;

        public bool IsPoliceSale => _sale.IsPoliceSale;

        public bool IsCreditCardSale => _sale.IsCreditCardSale;

        public string Time => _sale.Time.ToString("T");

        private void DoDismisItem()
        {
            var deleteSaleOption = new ActionSheetOption($"Отменить продажу товара {Name}", RejectSale);
            var utilizeSaleOption = new ActionSheetOption($"Списать продажу товара {Name}", UtilizeSale);
            UserDialogs.ActionSheet(new ActionSheetConfig()
            {
                Options = new List<ActionSheetOption>() {deleteSaleOption, utilizeSaleOption}
            });
        }

        private async void UtilizeSale()
        {          
            await productManager.UtilizeSaleProduct(_sale.Id);
            Publish(new AmoutChangedMessage(new Tuple<decimal, bool>(_sale.Amount, false), this));
            Publish(new SaleRemovedMessage(this));
            ShowSuccessMessage($"Списан товар {Name} !");
        }

        private async void RejectSale()
        {
            await productManager.DismisSaleProduct(_sale.Id);
            Publish(new AmoutChangedMessage(new Tuple<decimal, bool>(_sale.Amount, false), this));
            Publish(new SaleRemovedMessage(this));
            ShowSuccessMessage($"Отменена продажа товара {Name} !");           
        }
    }
}
