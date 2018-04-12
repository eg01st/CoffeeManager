using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;

namespace CoffeManager.Common.Providers
{
    public class CoffeeCounterProvider : BaseServiceProvider, ICoffeeCounterProvider
    {
        public async Task<IEnumerable<CoffeeCounterForCoffeeRoomDTO>> GetCounters()
        {
            return await Get<IEnumerable<CoffeeCounterForCoffeeRoomDTO>>(RoutesConstants.GetCounters);
        }

        public async Task<CoffeeCounterForCoffeeRoomDTO> GetCounter(int counterId)
        {
            return await Get<CoffeeCounterForCoffeeRoomDTO>(RoutesConstants.GetCounter, new Dictionary<string, string>()
            {
                {nameof(counterId), counterId.ToString()}
            });
        }

        public async Task<int> AddCounter(CoffeeCounterForCoffeeRoomDTO counter)
        {
            return await Put<int, CoffeeCounterForCoffeeRoomDTO>(RoutesConstants.AddCounter, counter);
        }

        public async Task UpdateCounter(CoffeeCounterForCoffeeRoomDTO counter)
        {
            await Post(RoutesConstants.UpdateCounter, counter);
        }

        public async Task DeleteCounter(int counterId)
        {
            await Delete(RoutesConstants.DeleteCounter, new Dictionary<string, string>()
            {
                {nameof(counterId), counterId.ToString()}
            });
        }
    }
}