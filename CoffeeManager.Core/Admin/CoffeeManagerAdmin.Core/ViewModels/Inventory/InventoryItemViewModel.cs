using System;
using CoffeeManager.Models;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Inventory
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

        protected override async void DoGoToDetails()
        {
            await NavigationService.Navigate<InventoryReportDetailsViewModel, int>(Id);
        }
    }
}
