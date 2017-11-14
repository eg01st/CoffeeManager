using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface IUserManager
    {
        Task<int> AddUser(UserDTO user);

        Task<IEnumerable<UserDTO>> GetUsers();

        Task ToggleEnabled(int userId);

        Task<UserDTO> GetUser(int userId);

        Task PaySalary(int userId, int coffeeRoomToPay);

        Task UpdateUser(UserDTO user);

        Task PenaltyUser(int userId, decimal amount, string reason);

        Task DismissPenalty(int id);
    }
}
