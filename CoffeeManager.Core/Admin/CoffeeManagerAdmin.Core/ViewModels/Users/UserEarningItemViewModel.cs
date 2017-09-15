using System;
using CoffeManager.Common;
using CoffeeManager.Models;
namespace CoffeeManagerAdmin.Core
{
    public class UserEarningItemViewModel : ListItemViewModelBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShiftId { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
        public string ShiftType { get; set; }

        public UserEarningItemViewModel(UserEarningsHistory item)
        {
            Id = item.Id;
            UserId = item.UserId;
            ShiftId = item.ShiftId;
            Amount = item.Amount;
            Date = item.Date.Date.ToString("dd-MM");
            ShiftType = item.IsDayShift ? "День" : "Ночь";
        }
    }
}
