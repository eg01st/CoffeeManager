using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ServiceProviders
{
    public class CupsServiceProvider : BaseServiceProvider
    {
        private const string Cups = "cups";

        public async Task<Cup[]> GetSupportedCups()
        {
            return await Get<Cup[]>(Cups);
        }

        public async Task UtilizeCup(int id, int shiftId)
        {
            await Put(Cups, id, new Dictionary<string, string>()
            {
                {nameof(shiftId), shiftId.ToString()}
            });
        }
    }
}
