using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.User;
using RestSharp.Portable;

namespace CoffeManager.Common
{
    public class UserServiceProvider : ServiceBase, IUserServiceProvider
    {
        public async Task<IEnumerable<User>> GetUsers()
        {
            var request = CreateGetRequest(RoutesConstants.GetUsers);
            return await ExecuteRequestAsync<IEnumerable<User>>(request);
        }

        public async Task<User> GetUser(int userId)
        {
            var request = CreateGetRequest(RoutesConstants.GetUser);
            request.Parameters.Add(new Parameter() { Name = nameof(userId), Value = userId });
            return await ExecuteRequestAsync<User>(request);
        }


        public async Task DeleteUser(int userId)
        {
            var request = CreateDeleteRequest(RoutesConstants.DeleteUser);
            request.Parameters.Add(new Parameter() { Name = nameof(userId), Value = userId });
            await ExecuteRequestAsync(request);
        }


        public async Task<int> AddUser(User user)
        {
            var request = CreatePutRequest(RoutesConstants.AddUser);
            request.AddBody(user);
            return await ExecuteRequestAsync<int>(request);
        }

        public async Task ToggleEnabled(int userId)
        {
            var request = CreatePostRequest(RoutesConstants.ToggleUserEnabled);
            request.Parameters.Add(new Parameter() { Name = nameof(userId), Value = userId });
            await ExecuteRequestAsync(request);
        }

        public async Task UpdateUser(User user)
        {
            var request = CreatePostRequest(RoutesConstants.UpdateUser);
            request.AddBody(user);
            await ExecuteRequestAsync(request);
        }

        public async Task PaySalary(int userId, int coffeeRoomToPay)
        {
            var request = CreatePostRequest(RoutesConstants.PaySalary);
            request.Parameters.Add(new Parameter() { Name = nameof(userId), Value = userId });
            request.Parameters.Add(new Parameter() { Name = nameof(coffeeRoomToPay), Value = coffeeRoomToPay });
            await ExecuteRequestAsync(request);
        }

        public async Task PenaltyUser(int userId, decimal amount, string reason)
        {
            var request = CreatePostRequest(RoutesConstants.PenaltyUser);
            request.AddBody(new PenaltyUserDTO() { Amount = amount, Reason = reason, UserId = userId });
            await ExecuteRequestAsync(request);
        }

        public async Task DismissPenalty(int id)
        {
            var request = CreatePostRequest(RoutesConstants.PenaltyUser);
            request.Parameters.Add(new Parameter() { Name = nameof(id), Value = id });
            await ExecuteRequestAsync(request);
        }

    }
}
