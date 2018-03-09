using System;
using CoffeeManager.Models;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Platform;
namespace CoffeeManagerAdmin.Core
{
    public class InventoryItemViewModel : ListItemViewModelBase
    {
        public DateTime Date { get; set; }
        public int Id { get; set; }

        public InventoryItemViewModel(InventoryReport s)
        {
            Date = s.Date;
            Id = s.Id;
        }

        protected override void DoGoToDetails()
        {
            ShowViewModel<InventoryReportDetailsViewModel>(new {id = Id});
        }
    }
}
