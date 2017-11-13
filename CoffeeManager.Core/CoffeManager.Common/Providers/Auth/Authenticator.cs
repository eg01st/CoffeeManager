using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp.Portable;
using CoffeManager.Common.Providers;

namespace CoffeManager.Common
{
    public class Authenticator : IAuthenticator
    {
        readonly IAccountProvider tokenService;

        public Authenticator(IAccountProvider tokenService)
        {
            this.tokenService = tokenService;
        }

        public OAuthToken UserToken { get; set; }

        public bool CanHandleChallenge(IHttpClient client, IHttpRequestMessage request, ICredentials credentials, IHttpResponseMessage response)
        {
            throw new NotImplementedException();
        }

        public bool CanPreAuthenticate(IRestClient client, IRestRequest request, ICredentials credentials)
        {
            throw new NotImplementedException();
        }

        public bool CanPreAuthenticate(IHttpClient client, IHttpRequestMessage request, ICredentials credentials)
        {
            throw new NotImplementedException();
        }

        public Task HandleChallenge(IHttpClient client, IHttpRequestMessage request, ICredentials credentials, IHttpResponseMessage response)
        {
            throw new NotImplementedException();
        }

        public async Task PreAuthenticate(IRestClient client, IRestRequest request, ICredentials credentials)
        {
            if (string.IsNullOrWhiteSpace(UserToken?.AccessToken) || UserToken.ExpirationDate <= DateTime.UtcNow)
            {
                var networkCredentials = credentials as NetworkCredential;

                var token = await tokenService.Authorize(networkCredentials.UserName, networkCredentials.Password);
                UserToken = token;
            }

            request?.AddHeader(HttpRequestHeader.Authorization.ToString(), $"{UserToken.TokenType} {UserToken.AccessToken}");
        }

        public Task PreAuthenticate(IHttpClient client, IHttpRequestMessage request, ICredentials credentials)
        {
            throw new NotImplementedException();
        }
    }
}
