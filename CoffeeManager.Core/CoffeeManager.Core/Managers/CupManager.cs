using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Core.ServiceProviders;
using CoffeeManager.Models;

namespace CoffeeManager.Core.Managers
{
    public class CupManager : BaseManager
    {
        private CupsServiceProvider provider = new CupsServiceProvider();
        public async Task<CupType[]> GetSupportedCups()
        {
            var cupTypes = await provider.GetSupportedCups();
            return cupTypes.Skip(1).ToArray();
        }

        public async Task UtilizeCup(int id)
        {
            await provider.UtilizeCup(id, ShiftNo);
        }
    }
}
