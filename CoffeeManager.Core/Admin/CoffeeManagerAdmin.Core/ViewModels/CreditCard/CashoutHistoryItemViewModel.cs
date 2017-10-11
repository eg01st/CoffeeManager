using System;
using CoffeManager.Common;
using CoffeeManager.Models;
namespace CoffeeManagerAdmin.Core
{
    public class CashoutHistoryItemViewModel : ListItemViewModelBase
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }

        public CashoutHistoryItemViewModel(CashoutHistory item)
        {
            Id = item.Id;
            Amount = item.Amount;
            Date = item.Date.ToString("dd-MM HH:mm");
        }
    }
}
