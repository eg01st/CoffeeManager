using System;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common.Providers
{
    public interface IShiftServiceProvider
    {
        Task<int> StartUserShift(int userId, int counter, DateTime startTime);

        Task<Shift> GetCurrentShift();

        Task<Shift> GetCurrentShiftForCoffeeRoom(int forCoffeeRoom);

        Task<Sale[]> GetCurrentShiftSales();

        Task<EndShiftUserInfo> EndShift(int shiftId, decimal realAmount, int endCounter);

        Task<ShiftInfo[]> GetShifts(int skip);

        Task<Sale[]> GetShiftSales(int id);

        Task<ShiftInfo> GetShiftInfo(int id);

        Task DiscardShift(int shiftId);
    }
}
