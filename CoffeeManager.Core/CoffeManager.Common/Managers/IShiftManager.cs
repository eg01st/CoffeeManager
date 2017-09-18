﻿using System;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface IShiftManager
    {
        Task<decimal> GetEntireMoney();

        Task<decimal> GetCurrentShiftMoney();

        Task<ShiftInfo[]> GetShifts();

        Task<Sale[]> GetShiftSales(int id);

        Task<ShiftInfo> GetShiftInfo(int id);

        Task<int> StartUserShift(int userId, int counter);

        Task<EndShiftUserInfo> EndUserShift(int shiftId, decimal realAmount, int endCounter);

        Task<Shift> GetCurrentShift();

        Task<Shift> GetCurrentShiftAdmin();

        Task<Sale[]> GetCurrentShiftSales();
    }
}
