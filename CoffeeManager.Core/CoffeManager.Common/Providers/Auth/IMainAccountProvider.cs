using System.Net;
using System.Threading.Tasks;
using CoffeeManager.Models;
using RestSharp.Portable;

namespace CoffeManager.Common.Providers.Auth
{
    public interface IMainAccountProvider
    {
        IAuthenticator Authenticator { get; set; }

        ICredentials Credentials { get; set; }

       // Task<OAuthToken> Authorize(string login, string password);

        Task<UserAcount> GetUserInfo();

        Task Register(string email, string password, string apiUrl);

        Task Logout();

        Task ChangePassword(string oldPassword, string newPassword);

        Task<UserAcount[]> GetUsers();

        Task DeleteAdminUser(string userId);

    }
}
