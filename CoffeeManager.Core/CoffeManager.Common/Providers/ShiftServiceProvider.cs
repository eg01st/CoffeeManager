using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Common;

namespace CoffeManager.Common
{
    public class ShiftServiceProvider : BaseServiceProvider, IShiftServiceProvider
    {
        public async Task<int> StartUserShift(int userId, int counter)
        {
            var result =
                await
                Post<Shift, object>(RoutesConstants.Shift, null,
                        new Dictionary<string, string>()
                        {
                            {nameof(userId), userId.ToString()},
                            {nameof(counter), counter.ToString()}
                        });
            return result.Id;
        }


        public async Task<Shift> GetCurrentShift()
        {
            return await Get<Shift>(RoutesConstants.GetCurrentShift);
        }

        public async Task<Sale[]> GetCurrentShiftSales()
        {
            return await Get<Sale[]>(RoutesConstants.GetShiftSales);
        }

        public async Task<EndShiftUserInfo> EndShift(int shiftId, decimal realAmount, int endCounter)
        {
            return await
                Post<EndShiftUserInfo, EndShiftDTO>(RoutesConstants.EndShift,
                     new EndShiftDTO()
                     {
                        CoffeeRoomNo = Config.CoffeeRoomNo,
                         ShiftId = shiftId,
                         RealAmount = realAmount,
                         Counter = endCounter
                     });
        }



        public async Task<ShiftInfo[]> GetShifts()
        {
            return await Get<ShiftInfo[]>(RoutesConstants.GetShifts);
        }

        public async Task<Sale[]> GetShiftSales(int id)
        {
            return await Get<Sale[]>(RoutesConstants.GetShiftSalesById, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task<ShiftInfo> GetShiftInfo(int id)
        {
            return await Get<ShiftInfo>(RoutesConstants.GetShiftInfo, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task<decimal> GetEntireMoney()
        {
            return await Get<decimal>(RoutesConstants.GetEntireMoney);
        }

        public async Task<decimal> GetCurrentShiftMoney()
        {
            return await Get<decimal>(RoutesConstants.GetCurrentShiftMoney);
        }
    }
}
