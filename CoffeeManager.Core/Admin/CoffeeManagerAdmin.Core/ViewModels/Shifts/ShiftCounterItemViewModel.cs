using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Shifts
{
    public class ShiftCounterItemViewModel : FeedItemElementViewModel
    {
        public string SuplyProductName { get; set; }
        public int StartCounter { get; set; }
        public int EndCounter { get; set; }
        public int Diff { get; set; }

        public ShiftCounterItemViewModel(CoffeeCounterDTO dto)
        {
            SuplyProductName = dto.SuplyProductName;
            StartCounter = dto.StartCounter;
            EndCounter = dto.EndCounter;
            Diff = EndCounter - StartCounter;
        }
    }
}