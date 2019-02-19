using CoffeeManager.Models;
using MobileCore.ViewModels;

namespace CoffeeManager.Core.ViewModels.Inventory
{
    public class PartialInventoryItemViewModel : FeedItemElementViewModel
    {
        private string quantityString;

        public PartialInventoryItemViewModel(SupliedProduct item)
        {
            Entity = item;
        }

        public string QuantityString
        {
            get => quantityString;
            set
            {
                quantityString = value;
                decimal res;
                if(decimal.TryParse(quantityString, out res))
                {
                    Entity.Quatity = res;
                }
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsProceeded));
            }
        }

        public SupliedProduct Entity { get; private set; }

        public string Name => Entity.Name;

        public int SuplyProductId => Entity.Id;

        public string ExpenseNumerationName => Entity.ExpenseNumerationName;

        public decimal? ExpenseNumerationMultyplier => Entity.ExpenseNumerationMultyplier;

        public int CoffeeRoomNo => Entity.CoffeeRoomNo;

        public bool IsProceeded => !string.IsNullOrEmpty(quantityString);
    }
}