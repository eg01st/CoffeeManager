using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;
using Newtonsoft.Json.Linq;

namespace CoffeManager.Common.Providers
{
    public class AccountProvider : BaseServiceProvider, IAccountProvider
    {
        public async Task<string> Authorize(string login, string password)
        {
            _apiUrl = Config.AuthApiUrl;
            var result = await Post<object>(RoutesConstants.Token, null, new Dictionary<string, string>()
            {
                {"grant_type", "password"},
                {"username", login},
                {"password", password}
            });
            var obj = new JObject(result);
            var token = obj["access_token"].Value<string>();
            return token;
        }

        public async Task<UserAcount> GetUserInfo()
        {
            return await Get<UserAcount>(RoutesConstants.GetUserInfo);
        }

        public async Task Register(string email, string password)
        {
            await Post<object>(RoutesConstants.Token, new { Email = email, Password = password, ConfirmPassword = password });
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task ChangePassword(string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task SetPassword(string newPassword, string confirmPassword)
        {
            throw new NotImplementedException();
        }
    }
}
