using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;

namespace CoffeManager.Common.Providers
{
    public interface ICoffeeCounterProvider
    {
        Task<IEnumerable<CoffeeCounterDTO>> GetCountersForShift(int shiftId);
        Task<IEnumerable<CoffeeCounterForCoffeeRoomDTO>> GetCounters();
        Task<CoffeeCounterForCoffeeRoomDTO> GetCounter(int counterId);
        Task<int> AddCounter(CoffeeCounterForCoffeeRoomDTO counter);
        Task UpdateCounter(CoffeeCounterForCoffeeRoomDTO counter);
        Task DeleteCounter(int counterId);
        
        Task ToggleIsActiveCounter(int id);
    }
}