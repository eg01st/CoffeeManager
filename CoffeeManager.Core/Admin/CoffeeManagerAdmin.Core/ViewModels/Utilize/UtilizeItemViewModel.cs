using System;
using CoffeManager.Common;
using CoffeeManager.Models;
namespace CoffeeManagerAdmin.Core
{
    public class UtilizeItemViewModel : ListItemViewModelBase
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public decimal Quantity { get; set; }
        public string Reason { get; set; }

        public UtilizeItemViewModel(UtilizedSuplyProduct product)
        {
            Id = product.Id;
            Name = product.SuplyProductName;
            Date = product.Date.ToString("dd-MM HH:mm");
            Quantity = product.Quantity;
            Reason = product.Reason;
        }

        protected override void DoGoToDetails()
        {
            Alert(Reason, "Причина:");
        }
    }
}
