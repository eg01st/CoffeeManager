using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Core.ServiceProviders;
using CoffeeManager.Core.ViewModels;
using CoffeeManager.Models;

namespace CoffeeManager.Core.Managers
{
    public class ShiftManager : BaseManager
    {
        private ShiftServiceProvider provider = new ShiftServiceProvider();
        public async Task<int> StartUserShift(int userId)
        {
            return await provider.StartUserShift(userId).ContinueWith((shiftId) =>
            {
                ShiftNo = shiftId.Result;
                return ShiftNo;
            });

        }

        public async Task EndUserShift(int shiftId)
        {
            await provider.EndShift(shiftId);
        }

        public async Task<Shift> GetCurrentShift()
        {
            var task = await provider.GetCurrentShift().ContinueWith(async shift =>
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
            return await provider.GetCurrentShiftSales();
        }
    }
}
