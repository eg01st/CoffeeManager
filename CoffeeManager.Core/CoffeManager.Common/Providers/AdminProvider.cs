using System;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public class AdminProvider : BaseServiceProvider, IAdminProvider
    {
        public async Task<Entity[]> GetCoffeeRooms()
        {
            return await Get<Entity[]>(RoutesConstants.GetCoffeeRooms);
        }
    }
}
