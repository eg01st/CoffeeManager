using System.Threading.Tasks;
using CoffeeManager.Core.ServiceProviders;
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
            return await provider.GetCurrentShift().ContinueWith((shift) =>
            {
                ShiftNo = shift.Result.Id;
                return shift.Result;
            }); 
        }
    }
}
