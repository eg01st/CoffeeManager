using System;
using CoffeeManager.Models;
using CoffeManager.Common;
namespace CoffeeManagerAdmin.Core
{
    public class StatisticSaleItemViewModel : ListItemViewModelBase
    {
        public StatisticSaleItemViewModel(SaleInfo sale)
        {
            Name = sale.Name;
            Amount = sale.Amount.Value.ToString("F");
            Quantity = sale.Quantity.ToString();
        }

        public StatisticSaleItemViewModel(Sale sale)
        {          

            Name = sale.Product1.Name;
            Amount = sale.Amount.ToString("F");
            Time = sale.Time.ToString("HH:mm:ss");
        }

        public string Amount {get;set;}
        public string Time {get;set;}
        public string Quantity {get;set;}
    }
}
