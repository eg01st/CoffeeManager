using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManager.Core.Managers
{
    public class DeptManager : BaseManager
    {
        private DeptProvider provider = new DeptProvider();
        public async Task AddDept(float amount)
        {
           await provider.AddDept(amount, ShiftNo);
        }

        public async Task RemoveDept(float amount)
        {
            await provider.RemoveDept(amount, ShiftNo);
        }

        public async Task<float> GetCurrentDeptSum()
        {
            return await provider.GetCurrentDeptSum();
        }
    }
}
