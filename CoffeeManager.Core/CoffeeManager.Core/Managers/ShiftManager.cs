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
        public async Task<int> StartUserShift(int userId, int counter)
        {
            return await provider.StartUserShift(userId, counter).ContinueWith((shiftId) =>
            {
                ShiftNo = shiftId.Result;
                return ShiftNo;
            });

        }

        public async Task EndUserShift(int shiftId, decimal realAmount, int endCounter)
        {
            await provider.EndShift(shiftId, realAmount, endCounter);
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

        public async Task AssertShiftSales(SaleStorage st)
        {
            await provider.AssertShiftSales(st);
        }
    }
}
