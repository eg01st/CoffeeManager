﻿using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public class ShiftManager : BaseManager, IShiftManager
    {
        private readonly IShiftServiceProvider shiftProvider;
        readonly ISyncManager syncManager;

        public ShiftManager(IShiftServiceProvider provider, ISyncManager syncManager)
        {
            this.syncManager = syncManager;
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
            var shiftId = await shiftProvider.StartUserShift(userId, counter);
            syncManager.AddCurrentShift(new ShiftEntity() { Id = shiftId, UserId = userId });
            ShiftNo = shiftId;
            return shiftId;

        }

        public async Task<EndShiftUserInfo> EndUserShift(int shiftId, decimal realAmount, int endCounter)
        {
            syncManager.ClearCurrentShift();
            return await shiftProvider.EndShift(shiftId, realAmount, endCounter);
        }

        public async Task<Shift> GetCurrentShift()
        {
            try
            {
                var shift = await shiftProvider.GetCurrentShift();
                if (shift != null)
                {
                    ShiftNo = shift.Id;
                }
                return shift;
            }
            catch(HttpRequestException wex)
            {
                var shiftDb = syncManager.GetCurrentShift();
                if(shiftDb != null)
                {
                    return (Shift)shiftDb;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToDiagnosticString());
                return null;
            }
        }

        public async Task<Sale[]> GetCurrentShiftSales()
        {
            return await shiftProvider.GetCurrentShiftSales();
        }
    }
}
