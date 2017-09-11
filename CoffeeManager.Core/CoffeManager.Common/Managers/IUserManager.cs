using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface IUserManager
    {
        Task<string> Login(string name, string password);

        Task<int> AddUser(User user);

        Task<IEnumerable<User>> GetUsers();

        Task ToggleEnabled(int userId);

        Task<User> GetUser(int userId);

        Task PaySalary(int userId, int currentShifId);

        Task UpdateUser(User user);

        Task PenaltyUser(int userId, decimal amount, string reason);

        Task DismissPenalty(int id);
    }
}
