using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Core.Managers;
using CoffeeManager.Core.Messages;
using CoffeeManager.Models;
using CoffeManager.Common;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class SaleViewModel : ViewModelBase
    {
        ProductManager ProductManager = new ProductManager();

        private ICommand _dismisItemCommand;
        private Sale _sale;
        private string _status;
        public SaleViewModel(Sale sale)
        {
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
        public string Name => _sale.Product1.Name;

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
            await ProductManager.UtilizeSaleProduct(_sale.Id);
            Publish(new AmoutChangedMessage(new Tuple<decimal, bool>(_sale.Amount, false), this));
            Publish(new SaleRemovedMessage(this));
            ShowSuccessMessage($"Списан товар {Name} !");
        }

        private async void RejectSale()
        {
            await ProductManager.DismisSaleProduct(_sale.Id);
            Publish(new AmoutChangedMessage(new Tuple<decimal, bool>(_sale.Amount, false), this));
            Publish(new SaleRemovedMessage(this));
            ShowSuccessMessage($"Отменена продажа товара {Name} !");           
        }
    }
}
