using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.ServiceProviders;
using CoffeeManager.Models;
using Newtonsoft.Json;

namespace CoffeeManager.Core.Managers
{
    public class DeptProvider : BaseServiceProvider
    {
        private const string Dept = "dept";
        public async Task AddDept(decimal amount, int shiftId)
        {
            await PutInternal(Dept, JsonConvert.SerializeObject(new Dept() {Amount = amount, CoffeeRoomNo = CoffeeRoomNo, ShiftId = shiftId, IsPaid = false}));
        }

        public async Task RemoveDept(decimal amount, int shiftId)
        {
            await PutInternal(Dept, JsonConvert.SerializeObject(new Dept() { Amount = amount, CoffeeRoomNo = CoffeeRoomNo, ShiftId = shiftId, IsPaid = true }));
        }

        public async Task<float> GetCurrentDeptSum()
        {
            return await Get<float>(Dept);
        }
    }
}
