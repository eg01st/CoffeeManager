using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeManager.Common.Providers;

namespace CoffeManager.Common.Managers
{
    public class AccountManager : BaseManager, IAccountManager
    {
        private readonly IAccountProvider _provider;

        public AccountManager(IAccountProvider provider)
        {
            _provider = provider;
        }

        public async Task<string> Authorize(string login, string password)
        {
            return await _provider.Authorize(login, password);
        }

        public async Task<UserAcount> GetUserInfo()
        {
            return await _provider.GetUserInfo();
        }

        public async Task Register(string email, string password)
        {
            await _provider.Register(email, password);
        }

        public async Task Logout()
        {
            await _provider.Logout();
        }

        public async Task ChangePassword(string oldPassword, string newPassword)
        {
            await _provider.ChangePassword(oldPassword, newPassword);
        }

        public async Task SetPassword(string newPassword, string confirmPassword)
        {
            await _provider.SetPassword(newPassword, confirmPassword);
        }
    }
}
