using System;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public class ShiftManager : BaseManager, IShiftManager
    {
        private readonly IShiftServiceProvider shiftProvider;

        public ShiftManager(IShiftServiceProvider provider)
        {
            this.shiftProvider = provider;
        }

        public async Task<decimal> GetEntireMoney()
        {
            return await shiftProvider.GetEntireMoney();
        }

        public async Task<decimal> GetCurrentShiftMoney()
        {
            return await shiftProvider.GetCurrentShiftMoney();
        }

        public async Task<ShiftInfo[]> GetShifts()
        {
            return await shiftProvider.GetShifts();
        }

        public async Task<Sale[]> GetShiftSales(int id)
        {
            return await shiftProvider.GetShiftSales(id);
        }

        public async Task<ShiftInfo> GetShiftInfo(int id)
        {
            return await shiftProvider.GetShiftInfo(id);
        }


        public async Task<int> StartUserShift(int userId, int counter)
        {
            return await shiftProvider.StartUserShift(userId, counter).ContinueWith((shiftId) =>
            {
                ShiftNo = shiftId.Result;
                return ShiftNo;
            });

        }

        public async Task<EndShiftUserInfo> EndUserShift(int shiftId, decimal realAmount, int endCounter)
        {
            return await shiftProvider.EndShift(shiftId, realAmount, endCounter);
        }

        public async Task<Shift> GetCurrentShift()
        {
            var task = await shiftProvider.GetCurrentShift().ContinueWith(async shift =>
            {
                var res = await shift;
                if (res != null)
                {
                    ShiftNo = res.Id;
                }
                return res;
            });
            return await task;
        }

        public async Task<Sale[]> GetCurrentShiftSales()
        {
            return await shiftProvider.GetCurrentShiftSales();
        }
    }
}
