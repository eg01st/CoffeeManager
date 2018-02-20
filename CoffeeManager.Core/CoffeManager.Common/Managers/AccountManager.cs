using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeManager.Common.Providers;
using CoffeeManager.Common;
using System;
using System.Linq;
using System.Net;
using CoffeManager.Common.Providers.Auth;

namespace CoffeManager.Common.Managers
{
    public class AccountManager : BaseManager, IAccountManager
    {
        private readonly ILocalAccountProvider localProvider;
        readonly IMainAccountProvider mainProvider;

        public AccountManager(ILocalAccountProvider provider, IMainAccountProvider mainProvider)
        {
            this.mainProvider = mainProvider;
            localProvider = provider;
        }


        public async Task<string> Authorize(string login, string password)
        {
            var credentials = new NetworkCredential(login, password);
            mainProvider.Credentials = credentials;
            var authenticator = new Authenticator((ITokenService)mainProvider);
            mainProvider.Authenticator = authenticator;

            var userInfo = await mainProvider.GetUserInfo();
            Config.ApiUrl = userInfo.ApiUrl;

            authenticator = new Authenticator((ITokenService)localProvider);
            localProvider.Authenticator = authenticator;
            localProvider.Credentials = credentials;
            ServiceBase.SetGlobalAuthenticator(authenticator);
            ServiceBase.SetGlobalCredentials(credentials);
           // BaseServiceProvider.SetAccessToken(token);

            return token;
        }

        public async Task<UserAcount> GetUserInfo()
        {
            return await mainProvider.GetUserInfo();
        }

        public async Task Register(string email, string password, string apiUrl)
        {
            await mainProvider.Register(email, password, apiUrl);
            await localProvider.Register(email, password, apiUrl);
        }


        public async Task Logout()
        {
            await mainProvider.Logout();
            await localProvider.Logout();
        }

        public async Task ChangePassword(string oldPassword, string newPassword)
        {
            await mainProvider.ChangePassword(oldPassword, newPassword);
            await localProvider.ChangePassword(oldPassword, newPassword);
        }

        public async Task<UserAcount[]> GetUsers()
        {
            return await localProvider.GetUsers();
        }


        public async Task DeleteAdminUser(string userId, string email, string localApiUrl)
        {
            var users = await localProvider.GetUsers();
            var localUser = users.FirstOrDefault(u => u.Email == email);
            if(localUser != null)
            {
                await localProvider.DeleteAdminUser(localUser.Id, localApiUrl);
                await mainProvider.DeleteAdminUser(userId);
            }
        }

    }
}
