using System.Threading.Tasks;
using CoffeeManager.Models;
using System.Net;
using RestSharp.Portable;

namespace CoffeManager.Common.Providers
{
    public interface IAccountProvider
    {
        IAuthenticator Authenticator { get; set; }

        ICredentials Credentials { get; set; }

        Task<OAuthToken> AuthorizeInitial(string login, string password);

        Task<OAuthToken> Authorize(string login, string password);

        Task<UserAcount> GetUserInfo();

        Task Register(string email, string password, string apiUrl);

        Task Logout();

        Task ChangePassword(string oldPassword, string newPassword);

        Task<UserAcount[]> GetUsers();

        Task<UserAcount[]> GetLocalUsers();

        Task RegisterLocal(string email, string password, string apiUrl);

        Task DeleteAdminUser(string userId);

        Task DeleteAdminUserLocal(string userId, string apiUrl);
    }
}
