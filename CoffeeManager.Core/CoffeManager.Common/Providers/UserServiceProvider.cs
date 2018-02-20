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
        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var request = CreateGetRequest(RoutesConstants.GetUsers);
            return await ExecuteRequestAsync<IEnumerable<UserDTO>>(request);
        }

        public async Task<UserDTO> GetUser(int userId)
        {
            var request = CreateGetRequest(RoutesConstants.GetUser);
            request.Parameters.Add(new Parameter() { Name = nameof(userId), Value = userId });
            return await ExecuteRequestAsync<UserDTO>(request);
        }


        public async Task DeleteUser(int userId)
        {
            var request = CreateDeleteRequest(RoutesConstants.DeleteUser);
            request.Parameters.Add(new Parameter() { Name = nameof(userId), Value = userId });
            await ExecuteRequestAsync(request);
        }


        public async Task<int> AddUser(UserDTO user)
        {
            var request = CreatePutRequest(RoutesConstants.AddUser);
            request.AddBody(user);
            return await ExecuteRequestAsync<int>(request);
        }

        public async Task ToggleEnabled(int userId)
        {
            var request = CreatePostRequest(RoutesConstants.ToggleUserEnabled);
            request.AddBody(new ToggleUserEnabledDTO() { UserId = userId});
            await ExecuteRequestAsync(request);
        }

        public async Task UpdateUser(UserDTO user)
        {
            var request = CreatePostRequest(RoutesConstants.UpdateUser);
            request.AddBody(user);
            await ExecuteRequestAsync(request);
        }

        public async Task PaySalary(int userId, int coffeeRoomToPay)
        {
            var request = CreatePostRequest(RoutesConstants.PaySalary);
            request.AddBody(new PaySalaryDTO() { UserId = userId, CoffeeRoomIdToPay = coffeeRoomToPay });
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
            var request = CreatePostRequest(RoutesConstants.DismisPenalty);
            request.AddBody(new DismissPenaltyDTO{ PenaltyId = id});
            await ExecuteRequestAsync(request);
        }

    }
}
