using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManagerAdmin.Core.ServiceProviders
{
    public class ShiftServiceProvider : BaseServiceProvider
    {
        private const string Shift = "shift";

        public async Task<Shift> GetCurrentShift()
        {
            return await Get<Shift>($"{Shift}/getCurrentShift");
        }

        public async Task<Sale[]> GetCurrentShiftSales()
        {
            return await Get<Sale[]>($"{Shift}/getShiftSales");
        }
        public async Task<decimal> GetEntireMoney()
        {
            return await Get<decimal>($"Payment/getentiremoney", null);
        }
        public async Task<decimal> GetCurrentShiftMoney()
        {
            return await Get<decimal>($"Payment/getcurrentshiftmoney", null);
        }
        public async Task<ShiftInfo[]> GetShifts()
        {
            return await Get<ShiftInfo[]>($"{Shift}/getShifts");
        }

        public async Task<Sale[]> GetShiftSales(int id)
        {
            return await Get<Sale[]>($"{Shift}/getShiftSalesById", new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task<ShiftInfo> GetShiftInfo(int id)
        {
            return await Get<ShiftInfo>($"{Shift}/getShiftInfo", new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }
    }
}
