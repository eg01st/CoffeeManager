using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

namespace CoffeManager.Common
{
    public class ServiceBase
    {
        public string BaseUrl { get; set; } = "http://csgoopencase-001-site3.atempurl.com/";

        private IRestClient GetClient()
        {
            var uri = new Uri(BaseUrl);
            var restClient = new RestClient();
            restClient.BaseUrl = uri;
            return restClient;
        }

        public ICredentials Credentials { get; set; }

        public IAuthenticator Authenticator { get; set; }


        protected IRestRequest CreateGetRequest(string resource)
        {
            return new RestRequest() { Method = Method.GET, Resource = resource };
        }

        protected IRestRequest CreatePostRequest(string resource)
        {
            return new RestRequest() { Method = Method.POST, Resource = resource };
        }

        protected IRestRequest CreatePutRequest(string resource)
        {
            return new RestRequest() { Method = Method.PUT, Resource = resource };
        }

        protected IRestRequest CreateDeleteRequest(string resource)
        {
            return new RestRequest() { Method = Method.DELETE, Resource = resource };
        }

        protected async Task<T> ExecuteRequestAsync<T>(IRestRequest request)
        {
            using(var client = GetClient())
            {
                if (Authenticator != null)
                {
                    client.Authenticator = Authenticator;
                    PreAuthenticate(client, request);
                }
                IRestResponse<T> response = await client.Execute<T>(request);

                return response.Data;
            }
        }


        private void PreAuthenticate(IRestClient client, IRestRequest request)
        {
            client.Authenticator.PreAuthenticate(client, request, Credentials);
        }

    }
}
