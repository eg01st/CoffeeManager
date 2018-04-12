using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;

namespace CoffeManager.Common.Providers
{
    public class ShiftServiceProvider : BaseServiceProvider, IShiftServiceProvider
    {
        public async Task<int> StartUserShift(int userId, List<CoffeeCounterDTO> couters, DateTime startTime)
        {
            var result =
                await
                Post<Shift, object>(RoutesConstants.Shift, couters,
                        new Dictionary<string, string>()
                        {
                            {nameof(userId), userId.ToString()},
                            {nameof(startTime), startTime.ToString("G")}
                        });
            return result.Id;
        }


        public async Task<Shift> GetCurrentShift()
        {
            return await Get<Shift>(RoutesConstants.GetCurrentShift);
        }

        public async Task<Shift> GetCurrentShiftForCoffeeRoom(int forCoffeeRoom)
        {
            return await Get<Shift>(RoutesConstants.GetCurrentShiftForCoffeeRoom, new Dictionary<string, string>() { { nameof(forCoffeeRoom), forCoffeeRoom.ToString() } });
        }

        public async Task<Sale[]> GetCurrentShiftSales()
        {
            return await Get<Sale[]>(RoutesConstants.GetShiftSales);
        }

        public async Task<EndShiftUserInfo> EndShift(int shiftId, decimal realAmount, List<CoffeeCounterDTO> couters)
        {
            return await
                Post<EndShiftUserInfo, EndShiftDTO>(RoutesConstants.EndShift,
                     new EndShiftDTO()
                     {
                         CoffeeRoomNo = Config.CoffeeRoomNo,
                         ShiftId = shiftId,
                         RealAmount = realAmount,
                         CoffeeCounters = couters
                     });
        }

        public async Task<ShiftInfo[]> GetShifts(int skip)
        {
            return await Get<ShiftInfo[]>(RoutesConstants.GetShifts, new Dictionary<string, string>() { { nameof(skip), skip.ToString() } });
        }

        public async Task<Sale[]> GetShiftSales(int id)
        {
            return await Get<Sale[]>(RoutesConstants.GetShiftSalesById, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task<ShiftInfo> GetShiftInfo(int id)
        {
            return await Get<ShiftInfo>(RoutesConstants.GetShiftInfo, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }
        
        public async Task DiscardShift(int shiftId)
        {
            await Delete(RoutesConstants.DiscardShift, new Dictionary<string, string>() { { nameof(shiftId), shiftId.ToString() } });
        }

    }
}
