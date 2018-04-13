using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;
using CoffeManager.Common.Providers;

namespace CoffeManager.Common.Managers
{
    public class CoffeeCounterManager : ICoffeeCounterManager
    {
        private readonly ICoffeeCounterProvider coffeeCounterProvider;

        public CoffeeCounterManager(ICoffeeCounterProvider coffeeCounterProvider)
        {
            this.coffeeCounterProvider = coffeeCounterProvider;
        }
        
        public async Task<IEnumerable<CoffeeCounterForCoffeeRoomDTO>> GetCounters()
        {
            return await coffeeCounterProvider.GetCounters();
        }

        public async Task<CoffeeCounterForCoffeeRoomDTO> GetCounter(int counterId)
        {
            return await coffeeCounterProvider.GetCounter(counterId);
        }

        public async Task<int> AddCounter(CoffeeCounterForCoffeeRoomDTO counter)
        {
            return await coffeeCounterProvider.AddCounter(counter);
        }

        public async Task UpdateCounter(CoffeeCounterForCoffeeRoomDTO counter)
        {
            await coffeeCounterProvider.UpdateCounter(counter);
        }

        public async Task DeleteCounter(int counterId)
        {
            await coffeeCounterProvider.DeleteCounter(counterId);
        }
    }
}