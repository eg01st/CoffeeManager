﻿using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;
using MobileCore.Connection;

namespace CoffeManager.Common
{
    public class ShiftManager : BaseManager, IShiftManager
    {
        private readonly IShiftServiceProvider shiftProvider;
        readonly ISyncManager syncManager;
        readonly IConnectivity connectivity;

        public ShiftManager(IShiftServiceProvider provider, ISyncManager syncManager, IConnectivity connectivity)
        {
            this.connectivity = connectivity;
            this.syncManager = syncManager;
            this.shiftProvider = provider;
        }


        public async Task<ShiftInfo[]> GetShifts(int skip)
        {
            return await shiftProvider.GetShifts(skip);
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
            if(!await connectivity.HasInternetConnectionAsync)
            {
                return TryGetShiftFromCache();
            }

            try
            {
                var shift = await shiftProvider.GetCurrentShift();
                if (shift != null)
                {
                    ShiftNo = shift.Id;
                    syncManager.ClearCurrentShift();
                    syncManager.AddCurrentShift(new ShiftEntity() { Id = shift.Id, UserId = shift.UserId });
                }
                return shift;
            }
            catch(HttpRequestException wex)
            {
                return TryGetShiftFromCache();
            }
            catch (TaskCanceledException tcex)
            {
                return TryGetShiftFromCache();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToDiagnosticString());
                await EmailService?.SendErrorEmail($"CoffeeRoomId: {Config.CoffeeRoomNo}",ex.ToDiagnosticString());
                return null;
            }
        }

        private Shift TryGetShiftFromCache()
        {
            var shiftDb = syncManager.GetCurrentShift();
            if (shiftDb != null)
            {
                ShiftNo = shiftDb.Id;
                return (Shift)shiftDb;
            }
            else
            {
                return null;
            }
        }

        public async Task<Shift> GetCurrentShiftAdmin()
        {
            try
            {
                var shift = await shiftProvider.GetCurrentShift();
                return shift;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToDiagnosticString());
                await EmailService?.SendErrorEmail($"CoffeeRoomId: {Config.CoffeeRoomNo}",ex.ToDiagnosticString());
                return null;
            }
        }

        public async Task<Sale[]> GetCurrentShiftSales()
        {
            return await shiftProvider.GetCurrentShiftSales();
        }

        public async Task<Shift> GetCurrentShiftForCoffeeRoom(int forCoffeeRoom)
        {
            return await shiftProvider.GetCurrentShiftForCoffeeRoom(forCoffeeRoom);
        }
    }
}
