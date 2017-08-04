using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public class UserServiceProvider : BaseServiceProvider, IUserServiceProvider
    {
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await Get<IEnumerable<User>>(RoutesConstants.GetUsers);
        }

        public async Task<User> GetUser(int userId)
        {
            return await Get<User>(RoutesConstants.GetUser, new Dictionary<string, string>()
                {
                    {nameof(userId), userId.ToString()}
                });
        }


        public async Task DeleteUser(int userId)
        {
            await Delete(RoutesConstants.DeleteUser, new Dictionary<string, string>()
                {
                    {nameof(userId), userId.ToString()}
                });
        }


        public async Task<string> Login(string name, string password)
        {
            return await Post(RoutesConstants.Login, new UserInfo() { Login = name, Password = password });
        }

        public async Task<int> AddUser(User user)
        {
            return await Put<int, User>(RoutesConstants.AddUser, user);
        }

        public async Task ToggleEnabled(int userId)
        {
            await Post<object>(RoutesConstants.ToggleUserEnabled, null, new Dictionary<string, string>()
                {
                    {nameof(userId), userId.ToString()}
                }
            );
        }

        public async Task UpdateUser(User user)
        {
            await Post<object>(RoutesConstants.UpdateUser, user);
        }

        public async Task PaySalary(int userId, int currentShifId)
        {
            await Post<object>(RoutesConstants.PaySalary, null, new Dictionary<string, string>()
                {
                    {nameof(userId), userId.ToString()},
                    {nameof(currentShifId), currentShifId.ToString()},
                }
            );
        }
    }
}
