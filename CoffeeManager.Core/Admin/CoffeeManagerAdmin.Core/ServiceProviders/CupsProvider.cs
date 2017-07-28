using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManagerAdmin.Core.ServiceProviders
{
    public class CupsProvider : BaseServiceProvider
    {
        public async Task<UsedCupPerShift> GetShiftUsedCups(int id)
        {
            return await Get<UsedCupPerShift>("cups/getshiftusedcups", new Dictionary<string, string>() {{nameof(id), id.ToString()}});
        }
    }
}
