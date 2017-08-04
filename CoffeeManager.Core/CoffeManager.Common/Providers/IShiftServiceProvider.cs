using System;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface IShiftServiceProvider
    {
        Task<int> StartUserShift(int userId, int counter);

        Task<Shift> GetCurrentShift();

        Task<Sale[]> GetCurrentShiftSales();

        Task<EndShiftUserInfo> EndShift(int shiftId, decimal realAmount, int endCounter);

        Task<ShiftInfo[]> GetShifts();

        Task<Sale[]> GetShiftSales(int id);

        Task<ShiftInfo> GetShiftInfo(int id);

        Task<decimal> GetEntireMoney();

        Task<decimal> GetCurrentShiftMoney();
    }
}
