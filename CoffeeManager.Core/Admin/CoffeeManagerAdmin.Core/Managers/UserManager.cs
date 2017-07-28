using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ServiceProviders;

namespace CoffeeManagerAdmin.Core.Managers
{
    public class UserManager : BaseManager
    {
        UserServiceProvider provider = new UserServiceProvider();
        public UserManager()
        {
        }

        public async Task<string> Login(string name, string password)
        {
            return await provider.Login(name, password);
        }

        public async Task<int> AddUser(User user)
        {
            return await provider.AddUser(user);
        }

        public async Task<List<User>> GetUsers()
        {
            return await provider.GetUsers();
        }

        public async Task ToggleEnabled(int userId)
        {
            await provider.ToggleEnabled(userId);
        }

        public async Task<User> GetUser(int userId)
        {
            return await provider.GetUser(userId);
        }

        public async Task PaySalary(int userId, int currentShifId)
        {
            await provider.PaySalary(userId, currentShifId);
        }

        public async  Task UpdateUser(User user)
        {
            await provider.UpdateUser(user);
        }
    }
}
