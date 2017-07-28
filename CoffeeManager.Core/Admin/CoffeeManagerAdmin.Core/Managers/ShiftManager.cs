using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ServiceProviders;

namespace CoffeeManagerAdmin.Core.Managers
{
    public class ShiftManager
    {
        private ShiftServiceProvider provider = new ShiftServiceProvider();
        public async Task<Shift> GetCurrentShift()
        {
            return await provider.GetCurrentShift();
        }
        public async Task<decimal> GetEntireMoney()
        {
            return await provider.GetEntireMoney();
        }

        public async Task<decimal> GetCurrentShiftMoney()
        {
            return await provider.GetCurrentShiftMoney();
        }

        public async Task<ShiftInfo[]> GetShifts()
        {
            return await provider.GetShifts();
        }

        public async Task<Sale[]> GetShiftSales(int id)
        {
            return await provider.GetShiftSales(id);
        }

        public async Task<ShiftInfo> GetShiftInfo(int id)
        {
            return await provider.GetShiftInfo(id);
        }
    }
}
