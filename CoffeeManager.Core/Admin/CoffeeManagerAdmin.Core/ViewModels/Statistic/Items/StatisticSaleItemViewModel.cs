using CoffeeManager.Models;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class StatisticSaleItemViewModel : FeedItemElementViewModel
    {
        public StatisticSaleItemViewModel(SaleInfo sale)
        {
            Name = sale.Name;
            Amount = sale.Amount.Value.ToString("F");
            Quantity = sale.Quantity.ToString();
        }

        public string Name { get; set; }
        public string Amount { get; set; }
        public string Quantity { get; set; }
    }
}
