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
            return new[]
            {
                new Sale()
                {
                    Amount = 11,
                    Id = 1,
                    IsPoliceSale = false,
                    ShiftId = 1,
                    Time = DateTime.Now,
                    Product = 1,
                    Product1 = new Product() {Id = 1, Name = "Prod1", Price = 11, PolicePrice = 6, ProductType = 1}
                },
                                new Sale()
                {
                    Amount = 22,
                    Id = 2,
                    IsPoliceSale = true,
                    ShiftId = 1,
                    Time = DateTime.Now,
                    Product = 1,
                    Product1 = new Product() {Id = 1, Name = "Prod2", Price = 22, PolicePrice = 6, ProductType = 2}
                },
            };
            return await provider.GetCurrentShiftSales();
        }
    }
}
