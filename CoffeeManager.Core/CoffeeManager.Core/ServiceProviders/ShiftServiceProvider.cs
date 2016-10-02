using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ServiceProviders
{
    public class ShiftServiceProvider : BaseServiceProvider
    {
        private const string Shift = "shift";
        public async Task<int> StartUserShift(int userId)
        {
            var result = await Post<Shift, Shift>(Shift, new Shift() {UserId = userId});
            return result.Id;
        }

        public async Task EndShift(int shiftId)
        {
            await Put(Shift, shiftId);
        }

        public async Task<Shift> GetCurrentShift()
        {
            try
            {
                return await Get<Shift>($"{Shift}/getCurrentShift");
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
