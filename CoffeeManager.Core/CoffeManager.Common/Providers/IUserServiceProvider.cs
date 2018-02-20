using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface IUserServiceProvider
    {
        Task<IEnumerable<UserDTO>> GetUsers();

        Task<UserDTO> GetUser(int userId);

        Task DeleteUser(int userId);

        Task<string> Login(string name, string password);

        Task<int> AddUser(UserDTO user);

        Task ToggleEnabled(int userId);

        Task UpdateUser(UserDTO user);

        Task PaySalary(int userId, int coffeeRoomToPay);

        Task PenaltyUser(int userId, decimal amount, string reason);

        Task DismissPenalty(int id);

        Task<decimal> GetSalaryAmountToPay();
    }
}
