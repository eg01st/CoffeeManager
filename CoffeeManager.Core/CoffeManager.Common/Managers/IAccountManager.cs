using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common.Managers
{
    public interface IAccountManager
    {
        Task<string> AuthorizeInitial(string login, string password);

        Task<UserAcount> Authorize(string login, string password);

        Task<UserAcount> GetUserInfo();

        Task<UserAcount[]> GetUsers();

        Task Register(string email, string password, string apiUrl);

        Task RegisterLocal(string email, string password, string apiUrl);

        Task Logout();

        Task ChangePassword(string oldPassword, string newPassword);

        Task DeleteAdminUser(string userId, string email, string localApiUrl);
    }
}
