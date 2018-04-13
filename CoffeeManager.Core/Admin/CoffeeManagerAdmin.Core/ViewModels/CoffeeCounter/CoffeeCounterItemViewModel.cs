using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.CoffeeCounter
{
    public class CoffeeCounterItemViewModel : FeedItemElementViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CoffeeCounterItemViewModel()
        {
            
        }

        public CoffeeCounterItemViewModel(CoffeeCounterForCoffeeRoomDTO dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            
            RaiseAllPropertiesChanged();
        }
        
        protected override async void Select()
        {
            await NavigationService.Navigate<CoffeeCounterDetailViewModel, int>(Id);

        }
    }
}