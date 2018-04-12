using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;

namespace CoffeManager.Common.Providers
{
    public interface IShiftServiceProvider
    {
        Task<int> StartUserShift(int userId, List<CoffeeCounterDTO> couters, DateTime startTime);

        Task<Shift> GetCurrentShift();

        Task<Shift> GetCurrentShiftForCoffeeRoom(int forCoffeeRoom);

        Task<Sale[]> GetCurrentShiftSales();

        Task<EndShiftUserInfo> EndShift(int shiftId, decimal realAmount, List<CoffeeCounterDTO> couters);

        Task<ShiftInfo[]> GetShifts(int skip);

        Task<Sale[]> GetShiftSales(int id);

        Task<ShiftInfo> GetShiftInfo(int id);

        Task DiscardShift(int shiftId);
    }
}
