using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CoffeManager.Common.Providers
{
    public class AccountProvider : ServiceBase, IAccountProvider
    {
        public async Task<OAuthToken> AuthorizeInitial(string login, string password)
        {
            BaseUrl = Config.AuthApiUrl;
            var request = CreatePostRequest(RoutesConstants.Token);
            request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = "grant_type", Value = "password" });
            request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = "username", Value = login });
            request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = "password", Value = password });

            var token = await ExecuteRequestAsync<OAuthTokenDTO>(request);
            return OAuthToken.FromDTO(token);
            //_apiUrl = Config.AuthApiUrl;
            //var result = await Post<string>(RoutesConstants.Token, new Dictionary<string, string>()
            //{
            //    {"grant_type", "password"},
            //    {"username", login},
            //    {"password", password},

            //});
            //var obj = JObject.Parse(result);
            //var token = obj["access_token"].Value<string>();
            //return token;
        }

        public async Task<OAuthToken> Authorize(string login, string password)
        {
            BaseUrl = Config.ApiUrl;
            var request = CreatePostRequest(RoutesConstants.Token);
            request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = "grant_type", Value = "password" });
            request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = "username", Value = login });
            request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = "password", Value = password });

            var token = await ExecuteRequestAsync<OAuthTokenDTO>(request);
            return OAuthToken.FromDTO(token);

            //_apiUrl = Config.ApiUrl;
            //var result = await Post<string>(RoutesConstants.Token, new Dictionary<string, string>() 
            //{
            //    {"grant_type", "password"},
            //    {"username", login},
            //    {"password", password},

            //} );
            //var obj = JObject.Parse(result);
            //var token = obj["access_token"].Value<string>();
            //return token;
        }

        public async Task<UserAcount> GetUserInfo()
        {
            return await ExecuteWithAdminCredentials(async () =>
            {
                return await Get<UserAcount>(RoutesConstants.GetUserInfo);
            }, false);

        }

        public async Task Register(string email, string password, string apiUrl)
        {
            await PostWithFullUrl<object>(Config.AuthApiUrl + RoutesConstants.Register, new { Email = email, Password = password, ConfirmPassword = password, ApiUrl = apiUrl });
        }

        public async Task RegisterLocal(string email, string password, string apiUrl)
        {
            await PostWithFullUrl<object>(apiUrl + RoutesConstants.Register, new { Email = email, Password = password, ConfirmPassword = password });
        }

        public async Task Logout()
        {
            await ExecuteWithAdminCredentials(
                Post<object>(RoutesConstants.Logout)
            );
        }

        public async Task ChangePassword(string oldPassword, string newPassword)
        {
            await ExecuteWithAdminCredentials(
                Post<string>(RoutesConstants.ChangePassword, new Dictionary<string, string>()
                {
                    {nameof(oldPassword), oldPassword},
                    {nameof(newPassword), newPassword},
                })
            );
        }

        public async Task<UserAcount[]> GetUsers()
        {
            return await ExecuteWithAdminCredentials<UserAcount[]>(async () =>
            {
                return await Get<UserAcount[]>(RoutesConstants.GetAdminUsers);
            }, false);
        }

        public async Task<UserAcount[]> GetLocalUsers()
        {
            return await Get<UserAcount[]>(RoutesConstants.GetAdminUsers);
        }

        public async Task DeleteAdminUser(string userId)
        {
            await PostWithFullUrl<object>(Config.AuthApiUrl + RoutesConstants.DeleteAdminUser, new { Id = userId } );
        }

        public async Task DeleteAdminUserLocal(string id, string apiUrl)
        {
            await PostWithFullUrl<object>(apiUrl + RoutesConstants.DeleteAdminUser, new {Id =id });
        }

    }
}
