using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Common;
using Newtonsoft.Json;

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

        public static void SetAccessToken(string token)
        {
            accessToken = token;
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
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

        protected async Task<T> Get<T>(string path, Dictionary<string, string> param = null)
        {
            string url = GetUrl(path, param);
            string responseString;
            var client = GetClient();

            using (var response = await client.GetAsync(url))
            {
                responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(response.ToString() + responseString);
                }
            }

            Debug.WriteLine(responseString);          
            var result = JsonConvert.DeserializeObject<T>(responseString);
            return result;
        }

        protected async Task<T> Post<T, TY>(string path, TY obj, Dictionary<string, string> param = null)
        {
            string url = GetUrl(path, param);
            string responseString;
            var client = GetClient();

            using (var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(obj))))
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

        protected async Task<string> Post<T>(string path, T obj, Dictionary<string, string> param = null)
        {
            string url = GetUrl(path, param);
            var client = GetClient();


            using (var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8,
                                        "application/json")))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
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
                                        "application/json")))
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
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

            using (var response = await client.PostAsync(url, new StringContent(body)))
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

            using (var response = await client.PostAsync(url, new StringContent(body)))
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

            using (var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(obj))))
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

            using (var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(obj))))
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
            using (var response = await client.DeleteAsync(url))
            { 
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(response.ToString() + responseString);
                }
                return responseString;
            }
        }

        private string GetUrl(string path, Dictionary<string, string> param = null)
        {
            string url = $"{_apiUrl}{path}?coffeeroomno={Config.CoffeeRoomNo}";
            if (param != null && param.Count > 0)
            {
                foreach (var parameter in param)
                {
                    url += $"&{parameter.Key}={parameter.Value}";
                }
            }
            return url;
        }

        private HttpClient GetClient()
        {
            if(httpClient == null)
            {
                httpClient = new HttpClient();
                httpClient.Timeout = new TimeSpan(0, 0, 20);
            }

            return httpClient;
        }
    }
}
