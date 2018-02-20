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

        public async Task PaySalary(int userId, int coffeeRoomToPay)
        {
            await Post<object>(RoutesConstants.PaySalary, null, new Dictionary<string, string>()
                {
                    {nameof(userId), userId.ToString()},
                    {nameof(coffeeRoomToPay), coffeeRoomToPay.ToString()},
                }
            );
        }

        public async Task PenaltyUser(int userId, decimal amount, string reason)
        {
            await Post<object>(RoutesConstants.PenaltyUser, null, new Dictionary<string, string>()
                {
                    {nameof(userId), userId.ToString()},
                    {nameof(amount), amount.ToString()},
                    {nameof(reason), reason.ToString()},
                }
            );
        }

        public async Task DismissPenalty(int id)
        {
            await Post<object>(RoutesConstants.DismisPenalty, null, new Dictionary<string, string>()
                {
                    {nameof(id), id.ToString()},
                }
            );
        }

        public async Task<decimal> GetSalaryAmountToPay()
        {
            return await Get<decimal>(RoutesConstants.GetSalaryAmountToPay);
        }
    }
}
