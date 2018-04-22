using CoffeeManager.Models;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Inventory.Create
{
    public class CreateInventoryItemViewModel : ListItemViewModelBase
    {
        private SupliedProduct item;

        public decimal? QuantityAfter { get; set; }

        public decimal QuantityBefore { get; set; }

        public int SuplyProductId { get; set; }

        public string InventoryNumerationName { get; set; }

        public decimal? InventoryNumerationMultyplier { get; set; }

        public int CoffeeRoomNo { get; set; }

        public bool IsProceeded { get; set; }

        public CreateInventoryItemViewModel(SupliedProduct item)
        {
            Name = item.Name;
            SuplyProductId = item.Id;
            QuantityBefore = item.Quatity ?? 0;
            CoffeeRoomNo = item.CoffeeRoomNo;
            InventoryNumerationName = item.InventoryNumerationName;
            InventoryNumerationMultyplier = item.InventoryNumerationMultyplier;
        }

        protected override async void DoGoToDetails()
        {
            var result = await PromtDecimalAsync($"Введите количество товара {Name} в единицах измерения '{InventoryNumerationName}'");
            if (result.HasValue)
            {
                IsProceeded = true;
                QuantityAfter = result.Value;
                RaisePropertyChanged(nameof(IsProceeded));
                RaisePropertyChanged(nameof(QuantityAfter));
                //Publish(new InventoryItemChangedMessage(this));
            }
        }
    }
}
