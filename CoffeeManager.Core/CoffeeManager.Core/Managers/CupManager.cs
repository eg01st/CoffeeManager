using System.Threading.Tasks;
using CoffeeManager.Core.ServiceProviders;
using CoffeeManager.Models;

namespace CoffeeManager.Core.Managers
{
    public class CupManager : BaseManager
    {
        private CupsServiceProvider provider = new CupsServiceProvider();
        public async Task<Cup[]> GetSupportedCups()
        {
            return await provider.GetSupportedCups();
        }

        public async Task UtilizeCup(int id)
        {
            await provider.UtilizeCup(id, ShiftNo);
        }
    }
}
