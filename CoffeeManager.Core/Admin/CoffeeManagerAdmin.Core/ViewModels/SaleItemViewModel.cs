using System;
using CoffeeManager.Models;
namespace CoffeeManagerAdmin.Core
{
    public class SaleItemViewModel : ListItemViewModelBase
    {
        public SaleItemViewModel(Sale sale)
        {
            IsCopSale = sale.IsPoliceSale;
            IsRejected = sale.IsRejected;
            IsUtilized = sale.IsUtilized;

            Name = sale.Product1.Name;
            Amount = sale.Amount.ToString("F");
            Time = sale.Time.ToString("HH:mm:ss");
            RaiseAllPropertiesChanged();
        }

        public SaleItemViewModel(SaleInfo sale)
        {
            Name = sale.Name;
            Amount = sale.Amount.Value.ToString("F");
            Quantity = sale.Quantity.ToString();
        }

        public bool IsCopSale {get;set;}
        public bool IsRejected {get;set;}
        public bool IsUtilized {get;set;}

        public string Name  {get;set;}
        public string Amount {get;set;}
        public string Time {get;set;}
        public string Quantity {get;set;}
    }
}
