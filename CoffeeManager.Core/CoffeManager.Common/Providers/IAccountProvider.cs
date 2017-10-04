using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common.Providers
{
    public interface IAccountProvider
    {
        Task<string> AuthorizeInitial(string login, string password);

        Task<string> Authorize(string login, string password);

        Task<UserAcount> GetUserInfo();

        Task Register(string email, string password);

        Task Logout();

        Task ChangePassword(string oldPassword, string newPassword);

        Task<UserAcount[]> GetUsers();

        Task SetApiUrl(string userId, string url);
    }
}
