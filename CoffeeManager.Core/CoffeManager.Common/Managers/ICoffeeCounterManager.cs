﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;

namespace CoffeManager.Common.Managers
{
    public interface ICoffeeCounterManager
    {
        Task<IEnumerable<CoffeeCounterForCoffeeRoomDTO>> GetCounters();
        Task<CoffeeCounterForCoffeeRoomDTO> GetCounter(int counterId);
        Task<int> AddCounter(CoffeeCounterForCoffeeRoomDTO counter);
        Task UpdateCounter(CoffeeCounterForCoffeeRoomDTO counter);
        Task DeleteCounter(int counterId);
    }
}