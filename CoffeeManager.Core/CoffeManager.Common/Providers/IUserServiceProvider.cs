using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.User;
using CoffeeManager.Models.User;

namespace CoffeManager.Common.Providers
{
    public interface IUserServiceProvider
    {
        Task<IEnumerable<UserDTO>> GetUsers();

        Task<UserDTO> GetUser(int userId);

        Task DeleteUser(int userId);

        Task<string> Login(string name, string password);

        Task<int> AddUser(UserDTO user);

        Task ToggleEnabled(ToggleUserEnabledDTO dto);

        Task UpdateUser(UserDTO user);

        Task PaySalary(PaySalaryDTO dto);

        Task PenaltyUser(PenaltyUserDTO dto);

        Task DismissPenalty(DismissPenaltyDTO dto);

        Task<decimal> GetSalaryAmountToPay();
    }
}
