using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.ServiceProviders;

namespace CoffeeManager.Core.Managers
{
    public class DeptProvider : BaseServiceProvider
    {
        private const string Dept = "dept";
        public async Task AddDept(float amount, int shiftId)
        {
            await Put<float>(Dept, amount, new Dictionary<string, string>()
            {
                {nameof(shiftId), shiftId.ToString()}
            });
        }

        public async Task RemoveDept(float amount, int shiftId)
        {
            await Delete(Dept, new Dictionary<string, string>()
            {
                {nameof(shiftId), shiftId.ToString()},
                {nameof(amount), amount.ToString() }
            });
        }

        public async Task<float> GetCurrentDeptSum()
        {
            return await Get<float>(Dept);
        }
    }
}
