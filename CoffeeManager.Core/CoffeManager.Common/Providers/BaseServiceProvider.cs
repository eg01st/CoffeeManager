using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Common;
using Newtonsoft.Json;
using MobileCore.Connection;
using MvvmCross.Platform;
using MobileCore.Extensions;

namespace CoffeManager.Common
{
    public class BaseServiceProvider
    {
        private static HttpClient httpClient;

        protected static string _apiUrl = Config.ApiUrl;


        public static string initialAccessToken;
        public static string accessToken;


        public static void SetInitialAccessToken(string accessToken)
        {
            initialAccessToken = accessToken;
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        }

        public static void SetAccessToken(string token, string apiUrl = null)
        {
            accessToken = token;
            var client = GetClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            if(apiUrl.IsNotNull())
            {
                _apiUrl = apiUrl;
            }
        }

        protected async Task ExecuteWithAdminCredentials(Task functionToRun)
        {
            Func<Task<bool>> runDelegate = async () => { await functionToRun; return true; };
            await ExecuteWithAdminCredentials(runDelegate);
        }

        protected async Task ExecuteWithAdminCredentials<T>(Func<Task<T>> functionToRun)
        {
            Func<Task<bool>> runDelegate = async () => { await functionToRun(); return true; };
            await ExecuteWithAdminCredentials(runDelegate);
        }


        protected async Task<T> ExecuteWithAdminCredentials<T>(Func<Task<T>> functionToRun, bool w = true)
        {
            try
            {
                SetInitialAccessToken(initialAccessToken);
                _apiUrl = Config.AuthApiUrl;

                var result = await functionToRun();
                return result;
            }
            finally
            {
                SetAccessToken(accessToken);
                _apiUrl = Config.ApiUrl;
            }
        }

        protected async Task<T> Get<T>(string path, Dictionary<string, string> param = null, int? coffeeRoomNo = null)
        {
            string url = GetUrl(path, param, coffeeRoomNo);
            string responseString;
            var client = GetClient();

            using (var response = await client.GetAsync(url).ConfigureAwait(false))
            {
                responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Debug.WriteLine("GET");
                    Debug.WriteLine(url);
                    throw new Exception(response.ToString() + responseString);
                }
            }

            Debug.WriteLine(responseString);
            if(responseString == null || responseString == "null")
            {
                return default(T);
            }
            var result = JsonConvert.DeserializeObject<T>(responseString);
            return result;
        }
        

        protected async Task<T> Post<T, TY>(string path, TY obj, Dictionary<string, string> param = null)
        {
            string url = GetUrl(path, param);
            string responseString;
            var client = GetClient();
            string body = JsonConvert.SerializeObject(obj);

 
            using (var response = await client.PostAsync(url, new StringContent(body)).ConfigureAwait(false))
            {
                responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Debug.WriteLine("Post");
                    Debug.WriteLine(url);
                    Debug.WriteLine(body);
                    throw new Exception(response.ToString() + responseString);
                }
            }

            var result = JsonConvert.DeserializeObject<T>(responseString);
            return result;

        }

        protected async Task<string> Post<T>(string path, T obj, Dictionary<string, string> param = null)
        {
            string url = GetUrl(path, param);
            var client = GetClient();
            string body = JsonConvert.SerializeObject(obj);


                using (var response = await client.PostAsync(url, new StringContent(body, Encoding.UTF8,
                                                                                    "application/json")).ConfigureAwait(false))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Debug.WriteLine("Post");
                    Debug.WriteLine(url);
                    Debug.WriteLine(body);

                    throw new Exception(response.ToString() + responseString);
                }

                return responseString;
            }

        }

        protected async Task<string> PostWithFullUrl<T>(string url, T obj)
        {
            using (var client = new HttpClient())
            {
                var stringContent = JsonConvert.SerializeObject(obj);


                using (var response = await client.PostAsync(url, new StringContent(stringContent, Encoding.UTF8,
                                        "application/json")).ConfigureAwait(false))
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        Debug.WriteLine("Post");
                        Debug.WriteLine(url);
                        Debug.WriteLine(stringContent);
                        throw new Exception(response.ToString() + responseString);
                    }
                    return responseString;
                }
            }

        }

        protected async Task<string> Post<T>(string path, Dictionary<string, string> param = null)
        {
            string url = GetUrl(path);
            var client = GetClient();

            string body = string.Empty;
            if (param != null && param.Count > 0)
            {
                foreach (var parameter in param)
                {
                    body += $"&{parameter.Key}={parameter.Value}";
                }
            }

            using (var response = await client.PostAsync(url, new StringContent(body)).ConfigureAwait(false))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    if(responseString.Contains("The user name or password is incorrect"))
                    {
                        throw new UnauthorizedAccessException(responseString);
                    }
                    else
                    {
                        Debug.WriteLine("Post");
                        Debug.WriteLine(url);
                        Debug.WriteLine(body);
                        throw new Exception(response.ToString() + responseString);
                    }
                }
                return responseString;
            }

        }

        protected async Task Post(string path, Dictionary<string, string> param = null)
        {
            string url = GetUrl(path);
            var client = GetClient();

            string body = string.Empty;
            if (param != null && param.Count > 0)
            {
                foreach (var parameter in param)
                {
                    body += $"&{parameter.Key}={parameter.Value}";
                }
            }

            using (var response = await client.PostAsync(url, new StringContent(body)).ConfigureAwait(false))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    if (responseString.Contains("The user name or password is incorrect"))
                    {
                        throw new UnauthorizedAccessException(responseString);
                    }
                    else
                    {
                        Debug.WriteLine("Post");
                        Debug.WriteLine(url);
                        Debug.WriteLine(body);
                        throw new Exception(response.ToString() + responseString);
                    }
                }
            }

        }

        protected async Task<T> Put<T, TY>(string path, TY obj, Dictionary<string, string> param = null)
        {
            string url = GetUrl(path, param);
            string responseString;
            var client = GetClient();
            var body = JsonConvert.SerializeObject(obj);
            Debug.WriteLine("PUT");
            Debug.WriteLine(url);
            Debug.WriteLine(body);
            using (var response = await client.PutAsync(url, new StringContent(body)).ConfigureAwait(false))
            {
                responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(response.ToString() + responseString);
                }
            }

            var result = JsonConvert.DeserializeObject<T>(responseString);
            return result;

        }

        protected async Task<string> Put<T>(string path, T obj, Dictionary<string, string> param = null)
        {
            string url = GetUrl(path, param);
            var client = GetClient();
            var body = JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { DateFormatString = "MM/dd/yy H:mm:ss"});
            Debug.WriteLine("PUT");
            Debug.WriteLine(url);
            Debug.WriteLine(body);
            using (var response = await client.PutAsync(url, new StringContent(body)).ConfigureAwait(false))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(response + responseString);
                }
                return responseString;
            }
        }

        protected async Task<string> Delete(string path, Dictionary<string, string> param = null)
        {
            string url = GetUrl(path, param);
            var client = GetClient();

            Debug.WriteLine("Delete");
            Debug.WriteLine(url);

            using (var response = await client.DeleteAsync(url).ConfigureAwait(false))
            { 
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(response.ToString() + responseString);
                }
                return responseString;
            }
        }

        private string GetUrl(string path, Dictionary<string, string> param = null, int? coffeeRoomId = null)
        {
            string url = $"{_apiUrl}{path}?coffeeroomno={coffeeRoomId ?? Config.CoffeeRoomNo}";
            if (param != null && param.Count > 0)
            {
                foreach (var parameter in param)
                {
                    url += $"&{parameter.Key}={parameter.Value}";
                }
            }
            return url;
        }

        private static HttpClient GetClient()
        {
            if(httpClient == null)
            {
                httpClient = new HttpClient();
                httpClient.Timeout = new TimeSpan(0, 0, 40);
            }

            return httpClient;
        }
    }
}
