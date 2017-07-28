using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ServiceProviders;

namespace CoffeeManagerAdmin.Core.Managers
{
    public class CupsManager : BaseManager
    {
        private CupsProvider provider = new CupsProvider();
        public async Task<UsedCupPerShift> GetShiftUsedCups(int id)
        {
            return await provider.GetShiftUsedCups(id);
        }
    }
}
