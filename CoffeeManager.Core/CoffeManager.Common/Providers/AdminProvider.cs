using System;
using System.Threading.Tasks;
using CoffeeManager.Models;
using System.Collections.Generic;
using RestSharp.Portable;

namespace CoffeManager.Common
{
    public class AdminProvider : ServiceBase, IAdminProvider
    {
        public async Task AddCoffeeRoom(string name)
        {
            var request = CreatePostRequest(RoutesConstants.AddCoffeeRoom);
            request.AddBody(new { Name = name });
            await ExecuteRequestAsync(request);

           // await Post(RoutesConstants.AddCoffeeRoom, new { Name = name });
        }

        public async Task DeleteCoffeeRoom(int id)
        {
            var request = CreatePostRequest(RoutesConstants.DeleteCoffeeRoom);
            request.AddBody(new { Id = id });
            await ExecuteRequestAsync(request);
            //await Post(RoutesConstants.DeleteCoffeeRoom, new { Id = id });
        }

        public async Task<Entity[]> GetCoffeeRooms()
        {
            var request = CreateGetRequest(RoutesConstants.GetCoffeeRooms);
            return await ExecuteRequestAsync<Entity[]>(request);
           // return await Get<Entity[]>(RoutesConstants.GetCoffeeRooms);
        }
    }
}
