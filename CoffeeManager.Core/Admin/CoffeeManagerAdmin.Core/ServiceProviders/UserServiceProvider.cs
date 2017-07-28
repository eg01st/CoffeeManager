using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManagerAdmin.Core.ServiceProviders
{
    public class UserServiceProvider : BaseServiceProvider
    {
        private const string Users = "Users";
        public UserServiceProvider()
        {
        }

        public async Task<string> Login(string name, string password)
        {
            return await Post($"{Users}/login", new UserInfo() { Login = name, Password = password });
        }

        public async Task<int> AddUser(User user)
        {
           return await Put<int, User>($"{Users}/add", user);
        }

        public async Task<List<User>> GetUsers()
        {
            return await Get<List<User>>(Users);
        }

        public async Task ToggleEnabled(int userId)
        {
            await Post<object>($"{Users}/toggleEnabled", null, new Dictionary<string, string>() 
                {
                    {nameof(userId), userId.ToString()}
                }
            );
        }

        public async Task<User> GetUser(int userId)
        {
             return await Get<User>($"{Users}/getUser", new Dictionary<string, string>() 
                {
                    {nameof(userId), userId.ToString()}
                });
        }

        public async Task UpdateUser(User user)
        {
           await Post<object>($"{Users}/update", user);
        }

        public async Task PaySalary(int userId, int currentShifId)
        {
            await Post<object>($"{Users}/paySalary", null, new Dictionary<string, string>() 
                {
                    {nameof(userId), userId.ToString()},
                    {nameof(currentShifId), currentShifId.ToString()},
                }
            );
        }
   }
}
