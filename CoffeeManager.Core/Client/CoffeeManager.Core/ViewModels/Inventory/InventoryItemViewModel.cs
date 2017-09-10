using System;
using CoffeeManager.Models;
using CoffeManager.Common;
namespace CoffeeManager.Core
{
    public class InventoryItemViewModel : ListItemViewModelBase
    {
        private SupliedProduct item;

        public decimal? QuantityAfter { get; set; }

        public decimal QuantityBefore { get; set; }

        public int SuplyProductId { get; set; }

        public bool IsProceeded { get; set; }

        public InventoryItemViewModel(SupliedProduct item)
        {
            this.item = item;
            Name = item.Name;
            SuplyProductId = item.Id;
            QuantityBefore = item.Quatity ?? 0;
        }

      

        protected async override void DoGoToDetails()
        {
            var result = await PromtDecimalAsync($"Введите количество товара {Name}");
            if(result.HasValue)
            {
                IsProceeded = true;
                QuantityAfter = result.Value;
                RaisePropertyChanged(nameof(IsProceeded));
                RaisePropertyChanged(nameof(QuantityAfter));
            }
        }
    }
}
