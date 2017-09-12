using System;
using CoffeManager.Common;
using CoffeeManager.Models;
namespace CoffeeManagerAdmin.Core
{
    public class UtilizeItemViewModel : ListItemViewModelBase
    {
        public string Date { get; set; }
        public decimal Quantity { get; set; }
        public string Reason { get; set; }

        public UtilizeItemViewModel(UtilizedSuplyProduct product)
        {
            Name = product.SuplyProductName;
            Date = product.Date.Date.ToString("dd-MM"); ;
            Quantity = product.Quantity;
            Reason = product.Reason;
        }

        protected override void DoGoToDetails()
        {
            Alert(Reason, "Причина:");
        }
    }
}
