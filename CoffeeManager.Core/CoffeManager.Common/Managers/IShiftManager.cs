using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;

namespace CoffeManager.Common.Managers
{
    public interface IShiftManager
    {
        Task<ShiftInfo[]> GetShifts(int skip);

        Task<Sale[]> GetShiftSales(int id);

        Task<ShiftInfo> GetShiftInfo(int id);

        Task<int> StartUserShift(int userId, List<CoffeeCounterDTO> couters);

        Task<EndShiftUserInfo> EndUserShift(int shiftId, decimal realAmount, List<CoffeeCounterDTO> couters);

        Task<Shift> GetCurrentShift();

        Task<Shift> GetCurrentShiftForCoffeeRoom(int forCoffeeRoom);

        Task<Shift> GetCurrentShiftAdmin();

        Task<Sale[]> GetCurrentShiftSales();

        Task DiscardShift(int shiftId);
    }
}
