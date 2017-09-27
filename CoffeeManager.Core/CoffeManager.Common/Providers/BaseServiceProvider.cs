using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeManager.Common;
using Newtonsoft.Json;

namespace CoffeManager.Common
{
    public class BaseServiceProvider
    {
        private static HttpClient httpClient;

        protected static string _apiUrl = Config.ApiUrl;

        public static void SetAccessToken(string accessToken)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
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

            using (var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(obj))))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(response.ToString() + responseString);
                }
                return responseString;
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
                httpClient.Timeout = new TimeSpan(0, 0, 5);
            }

            return httpClient;
        }
    }
}
