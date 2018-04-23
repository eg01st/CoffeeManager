using System;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ViewModels.SuplyProducts;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class InventoryReportDetailsItemViewModel : ListItemViewModelBase
    {

        public int Id { get; set; }
        public int SuplyProductId { get; set; }
        public string SuplyProductName { get; set; }
        public decimal QuantityBefore { get; set; }
        public decimal QuantityAfer { get; set; }
        public decimal QuantityDiff { get; set; }

        public InventoryReportDetailsItemViewModel()
        {
        }

        public InventoryReportDetailsItemViewModel(InventoryItem s)
        {
            Id = s.Id;
            SuplyProductName = s.SuplyProductName;
            SuplyProductId = s.SuplyProductId;
            QuantityBefore = s.QuantityBefore;
            QuantityAfer = s.QuantityAfer;
            QuantityDiff = s.QuantityDiff;
        }

        protected override async void DoGoToDetails()
        {
            await NavigationService.Navigate<SuplyProductDetailsViewModel, int>(SuplyProductId);
        }
    }
}
