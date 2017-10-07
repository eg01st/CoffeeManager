using System;
using System.Threading.Tasks;
using CoffeeManager.Models;
using System.Collections.Generic;

namespace CoffeManager.Common
{
    public class AdminProvider : BaseServiceProvider, IAdminProvider
    {
        public async Task AddCoffeeRoom(string name)
        {
            await Post(RoutesConstants.AddCoffeeRoom, new { Name = name });
        }

        public async Task DeleteCoffeeRoom(int id)
        {
            await Post(RoutesConstants.DeleteCoffeeRoom, new { Id = id });
        }

        public async Task<Entity[]> GetCoffeeRooms()
        {
            return await Get<Entity[]>(RoutesConstants.GetCoffeeRooms);
        }
    }
}
