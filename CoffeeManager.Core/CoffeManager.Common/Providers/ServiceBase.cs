using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using CoffeeManager.Common;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace CoffeManager.Common
{
    public class ServiceBase
    {
        private static IAuthenticator globalAuthenticator;
        private static ICredentials globalCredentials;

        public virtual string BaseUrl 
        { 
            get { return Config.ApiUrl; }
        }

        private IRestClient GetClient()
        {
            var uri = new Uri(BaseUrl);
            var restClient = new RestClient();
            restClient.BaseUrl = uri;
            return restClient;
        }

        public static void SetGlobalAuthenticator(IAuthenticator authenticator)
        {
            globalAuthenticator = authenticator;
        }

        public static void SetGlobalCredentials(ICredentials credentials)
        {
            globalCredentials = credentials;
        }

        public ICredentials Credentials { get; set; }

        public IAuthenticator Authenticator { get; set; }


        protected IRestRequest CreateGetRequest(string resource)
        {
            return AddCoffeeRoomParameter(new RestRequest() { Method = Method.GET, Resource = resource });
        }

        protected IRestRequest CreatePostRequest(string resource)
        {
            return AddCoffeeRoomParameter(new RestRequest() { Method = Method.POST, Resource = resource });
        }

        protected IRestRequest CreatePutRequest(string resource)
        {
            return AddCoffeeRoomParameter(new RestRequest() { Method = Method.PUT, Resource = resource });
        }

        protected IRestRequest CreateDeleteRequest(string resource)
        {
            return AddCoffeeRoomParameter(new RestRequest() { Method = Method.DELETE, Resource = resource });
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
                else if(globalAuthenticator != null)
                {
                    client.Authenticator = globalAuthenticator;
                    PreAuthenticate(client, request);
                }
                var url = client.BuildUri(request);
                IRestResponse<T> response = await client.Execute<T>(request);
                LogRequest(client, request, response);
                return response.Data;
            }
        }

        protected async Task ExecuteRequestAsync(IRestRequest request)
        {
            await ExecuteRequestAsync<bool>(request);
        }

        protected async Task<T> ExecuteRequestNoAuthAsync<T>(IRestRequest request)
        {
            using (var client = GetClient())
            {
                IRestResponse<T> response = await client.Execute<T>(request);
                return response.Data;
            }
        }

        private void PreAuthenticate(IRestClient client, IRestRequest request)
        {
            var credentials = Credentials ?? globalCredentials;
            client.Authenticator.PreAuthenticate(client, request, credentials);
        }

        private IRestRequest AddCoffeeRoomParameter(IRestRequest request)
        {
            request.AddQueryParameter("coffeeroomno", Config.CoffeeRoomNo);
            return request;
        }


        private void LogRequest(IRestClient client, IRestRequest request, IRestResponse response)
        {
            var requestToLog = new
            {
                resource = request.Resource,
                // Parameters are custom anonymous objects in order to have the parameter type as a nice string
                // otherwise it will just show the enum value
                parameters = request.Parameters.Select(parameter => new
                {
                    name = parameter.Name,
                    value = parameter.Value,
                    type = parameter.Type.ToString()
                }),
                // ToString() here to have the method as a nice string otherwise it will just show the enum value
                method = request.Method.ToString(),
                // This will generate the actual Uri used in the request
                uri = client.BuildUri(request),
            };

            var responseToLog = new
            {
                statusCode = response.StatusCode,
                content = response.Content,
                headers = response.Headers,
                // The Uri that actually responded (could be different from the requestUri if a redirection occurred)
                responseUri = response.ResponseUri,
               // errorMessage = response.ErrorMessage,
            };

            Debug.WriteLine(string.Format("Request completed in {0} ms, Request: {1}, Response: {2}",
                    0,
                    JsonConvert.SerializeObject(requestToLog),
                    JsonConvert.SerializeObject(responseToLog)));
        }

    }
}
