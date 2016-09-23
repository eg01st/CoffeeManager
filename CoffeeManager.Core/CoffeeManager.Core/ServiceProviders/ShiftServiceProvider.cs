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
            var result = await Post<Entity, Entity>(Shift, new Entity(CoffeeRommNo) {Id = userId});
            return result.Id;
        }

        public async Task EndShift(int shiftId)
        {
            await Put(Shift, new Entity(CoffeeRommNo) {Id = shiftId});
        }
    }
}
