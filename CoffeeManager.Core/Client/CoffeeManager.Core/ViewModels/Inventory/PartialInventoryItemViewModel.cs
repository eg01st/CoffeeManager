using CoffeeManager.Core.Messages;
using CoffeeManager.Models;
using CoffeManager.Common.ViewModels;

namespace CoffeeManager.Core.ViewModels.Inventory
{
    public class PartialInventoryItemViewModel : ListItemViewModelBase
    {        
        public PartialInventoryItemViewModel(SupliedProduct item)
        {
            Entity = item;
        }
        
        public decimal Quantity
        {
            get => Entity.Quatity ?? 0;
            set
            {
                Entity.Quatity = value; 
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsProceeded));
            }
        }
                        
        public SupliedProduct Entity { get; private set; }

        public int SuplyProductId => Entity.Id;

        public string ExpenseNumerationName => Entity.ExpenseNumerationName;

        public decimal? ExpenseNumerationMultyplier => Entity.ExpenseNumerationMultyplier;

        public int CoffeeRoomNo => Entity.CoffeeRoomNo;

        public bool IsProceeded => Quantity > -1;
    }
}