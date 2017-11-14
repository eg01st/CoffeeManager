using System;
using System.Net;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeManager.Common.Providers.Auth;
namespace CoffeManager.Common
{
    public class MainAccountProvider : ServiceBase, IMainAccountProvider, ITokenService
    {
        public override string BaseUrl => Config.AuthApiUrl;

        public MainAccountProvider()
        {
        }

        //public async Task<OAuthToken> Authorize(string login, string password)
        //{
        //    BaseUrl = Config.AuthApiUrl;
        //    var request = CreatePostRequest(RoutesConstants.Token);
        //    request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = "grant_type", Value = "password" });
        //    request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = "username", Value = login });
        //    request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = "password", Value = password });

        //    var token = await ExecuteRequestAsync<OAuthTokenDTO>(request);
        //    return OAuthToken.FromDTO(token);
        //}

        public Task ChangePassword(string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAdminUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserAcount> GetUserInfo()
        {
            var request = CreateGetRequest(RoutesConstants.GetUserInfoNew);
            return await ExecuteRequestAsync<UserAcount>(request);
        }

        public Task<UserAcount[]> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<OAuthToken> GetUserToken(ICredentials credintials)
        {
            var networkCredentials = Credentials as NetworkCredential;

            //BaseUrl = Config.AuthApiUrl;
            var request = CreatePostRequest(RoutesConstants.Token);
            request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = "grant_type", Value = "password" });
            request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = "username", Value = networkCredentials.UserName });
            request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = "password", Value = networkCredentials.Password });

            var token = await ExecuteRequestNoAuthAsync<OAuthTokenDTO>(request);
            return OAuthToken.FromDTO(token);
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task Register(string email, string password, string apiUrl)
        {
            throw new NotImplementedException();
        }
    }
}
