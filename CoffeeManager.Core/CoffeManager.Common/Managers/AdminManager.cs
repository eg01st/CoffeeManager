using System;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public class AdminManager : BaseManager, IAdminManager
    {
        readonly IAdminProvider provider;

        public AdminManager(IAdminProvider provider)
        {
            this.provider = provider;
        }

        public async Task<Entity[]> GetCoffeeRooms()
        {
            return await provider.GetCoffeeRooms();
        }
    }
}
