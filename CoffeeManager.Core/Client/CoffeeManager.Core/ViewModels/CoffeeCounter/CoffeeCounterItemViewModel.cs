using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;
using MobileCore.ViewModels;

namespace CoffeeManager.Core.ViewModels.CoffeeCounter
{
    public class CoffeeCounterItemViewModel : FeedItemElementViewModel
    {
        private int? counter;
        private int? confirm;

        public int Id { get; set; }
        public int SuplyProductId { get; set; }
        public string Name { get; set; }
        
        public int? Counter
        {
            get => counter;
            set
            {
                counter = value;
                RaisePropertyChanged();
            }
        }
        
        public int? Confirm
        {
            get => confirm;
            set
            {
                confirm = value;
                RaisePropertyChanged();
            }
        }

        public CoffeeCounterItemViewModel(CoffeeCounterForCoffeeRoomDTO dto)
        {
            Id = dto.Id;
            SuplyProductId = dto.SuplyProductId;
            Name = dto.Name;
            RaisePropertyChanged(nameof(Name));
        }
    }
}