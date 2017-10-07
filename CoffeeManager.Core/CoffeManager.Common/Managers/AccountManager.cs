using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeManager.Common.Providers;
using CoffeeManager.Common;
using System;
using System.Linq;

namespace CoffeManager.Common.Managers
{
    public class AccountManager : BaseManager, IAccountManager
    {
        private readonly IAccountProvider _provider;

        public AccountManager(IAccountProvider provider)
        {
            _provider = provider;
        }

        public async Task<string> AuthorizeInitial(string login, string password)
        {
            return await _provider.AuthorizeInitial(login, password);
        }

        public async Task<UserAcount> Authorize(string login, string password)
        {
            var initialAccessToken = await _provider.AuthorizeInitial(login, password);
            BaseServiceProvider.SetInitialAccessToken(initialAccessToken);
            var userInfo = await _provider.GetUserInfo();
            Config.ApiUrl = userInfo.ApiUrl;

            var token = await _provider.Authorize(login, password);
            BaseServiceProvider.SetAccessToken(token);

            return userInfo;
        }

        public async Task<UserAcount> GetUserInfo()
        {
            return await _provider.GetUserInfo();
        }

        public async Task Register(string email, string password, string apiUrl)
        {
            await _provider.Register(email, password, apiUrl);
        }

        public async Task RegisterLocal(string email, string password, string apiUrl)
        {
            await _provider.RegisterLocal(email, password, apiUrl);
        }

        public async Task Logout()
        {
            await _provider.Logout();
        }

        public async Task ChangePassword(string oldPassword, string newPassword)
        {
            await _provider.ChangePassword(oldPassword, newPassword);
        }

        public async Task<UserAcount[]> GetUsers()
        {
            return await _provider.GetUsers();
        }


        public async Task DeleteAdminUser(string userId, string email, string localApiUrl)
        {
            var users = await _provider.GetLocalUsers();
            var localUser = users.FirstOrDefault(u => u.Email == email);
            if(localUser != null)
            {
                await _provider.DeleteAdminUserLocal(localUser.Id, localApiUrl);
                await _provider.DeleteAdminUser(userId);
            }
        }
    }
}
