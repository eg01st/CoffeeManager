﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.User;
using CoffeeManager.Models.User;
using CoffeManager.Common.Providers;

namespace CoffeManager.Common.Managers
{
    public class UserManager : BaseManager, IUserManager
    {
        private readonly IUserServiceProvider provider;

        public UserManager(IUserServiceProvider provider)
        {
            this.provider = provider;
        }

        public async Task<string> Login(string name, string password)
        {
            return await provider.Login(name, password);
        }

        public async Task<int> AddUser(UserDTO user)
        {
            return await provider.AddUser(user);
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            return await provider.GetUsers();
        }

        public async Task ToggleEnabled(int userId)
        {
            await provider.ToggleEnabled(new ToggleUserEnabledDTO() { UserId = userId});
        }

        public async Task<UserDTO> GetUser(int userId)
        {
            return await provider.GetUser(userId);
        }

        public async Task PaySalary(int userId, int coffeeRoomToPay)
        {
            await provider.PaySalary(new PaySalaryDTO() {UserId = userId, CoffeeRoomIdToPay = coffeeRoomToPay});
        }

        public async Task UpdateUser(UserDTO user)
        {
            await provider.UpdateUser(user);
        }

        public async Task PenaltyUser(int userId, decimal amount, string reason)
        {
            await provider.PenaltyUser(new PenaltyUserDTO() { UserId = userId, Amount = amount, Reason = reason });
        }

        public async Task DismissPenalty(int id)
        {
            await provider.DismissPenalty(new DismissPenaltyDTO(){ PenaltyId = id});
        }

        public async Task<int> GetSalaryAmountToPay()
        {
            var result = await provider.GetSalaryAmountToPay();
            return (int)result;
        }
    }
}
