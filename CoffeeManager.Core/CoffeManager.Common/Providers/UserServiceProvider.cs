using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.User;

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

        public async Task ToggleEnabled(int userId)
        {
            await Post<object>(RoutesConstants.ToggleUserEnabled, null, new Dictionary<string, string>()
                {
                    {nameof(userId), userId.ToString()}
                }
            );
        }

        public async Task UpdateUser(UserDTO user)
        {
            await Post<object>(RoutesConstants.UpdateUser, user);
        }

        public async Task PaySalary(PaySalaryDTO dto)
        {
            await Post<PaySalaryDTO>(RoutesConstants.PaySalary, dto);
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
