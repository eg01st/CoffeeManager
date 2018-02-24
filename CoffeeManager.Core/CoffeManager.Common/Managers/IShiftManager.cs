using System;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface IShiftManager
    {
        Task<ShiftInfo[]> GetShifts(int skip);

        Task<Sale[]> GetShiftSales(int id);

        Task<ShiftInfo> GetShiftInfo(int id);

        Task<int> StartUserShift(int userId, int counter);

        Task<EndShiftUserInfo> EndUserShift(int shiftId, decimal realAmount, int endCounter);

        Task<Shift> GetCurrentShift();

        Task<Shift> GetCurrentShiftForCoffeeRoom(int forCoffeeRoom);

        Task<Shift> GetCurrentShiftAdmin();

        Task<Sale[]> GetCurrentShiftSales();

        Task DiscardShift(int shiftId);
    }
}
