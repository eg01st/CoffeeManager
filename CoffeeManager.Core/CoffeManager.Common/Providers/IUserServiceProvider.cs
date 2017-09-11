using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface IUserServiceProvider
    {
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUser(int userId);

        Task DeleteUser(int userId);

        Task<string> Login(string name, string password);

        Task<int> AddUser(User user);

        Task ToggleEnabled(int userId);

        Task UpdateUser(User user);

        Task PaySalary(int userId, int currentShifId);

        Task PenaltyUser(int userId, decimal amount, string reason);

        Task DismissPenalty(int id);
    }
}
