using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.User;
using CoffeeManager.Models.User;

namespace CoffeManager.Common.Providers
{
    public class UserServiceProvider : BaseServiceProvider, IUserServiceProvider
    {
        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            return await Get<IEnumerable<UserDTO>>(RoutesConstants.GetUsers);
        }

        public async Task<UserDTO> GetUser(int userId)
        {
            return await Get<UserDTO>(RoutesConstants.GetUser, new Dictionary<string, string>()
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

        public async Task<int> AddUser(UserDTO user)
        {
            return await Put<int, UserDTO>(RoutesConstants.AddUser, user);
        }

        public async Task ToggleEnabled(ToggleUserEnabledDTO dto)
        {
            await Post(RoutesConstants.ToggleUserEnabled, dto);
        }

        public async Task UpdateUser(UserDTO user)
        {
            await Post(RoutesConstants.UpdateUser, user);
        }

        public async Task PaySalary(PaySalaryDTO dto)
        {
            await Post(RoutesConstants.PaySalary, dto);
        }

        public async Task PenaltyUser(PenaltyUserDTO dto)
        {
            await Post(RoutesConstants.PenaltyUser, dto);
        }

        public async Task DismissPenalty(DismissPenaltyDTO dto)
        {
            await Post(RoutesConstants.DismisPenalty, dto);
        }

        public async Task<decimal> GetSalaryAmountToPay()
        {
            return await Get<decimal>(RoutesConstants.GetSalaryAmountToPay);
        }
    }
}
