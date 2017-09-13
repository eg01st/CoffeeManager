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
        public static string AccessToken = "";

        private readonly string _apiUrl = Config.ApiUrl;

        protected async Task<T> Get<T>(string path, Dictionary<string, string> param = null)
        {
            string url = GetUrl(path, param);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("token", AccessToken);
            var response = await client.GetAsync(url);
            string responseString = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(responseString);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.ToString() + responseString);
            }
            var result = JsonConvert.DeserializeObject<T>(responseString);
            return result;
        }

        protected async Task<T> Post<T, TY>(string path, TY obj, Dictionary<string, string> param = null)
        {
            string url = GetUrl(path, param);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("token", AccessToken);

            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(obj)));
            string responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.ToString() + responseString);
            }
            var result = JsonConvert.DeserializeObject<T>(responseString);
            return result;

        }

        protected async Task<string> Post<T>(string path, T obj, Dictionary<string, string> param = null)
        {
            string url = GetUrl(path, param);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("token", AccessToken);

            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(obj)));
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.ToString() + responseString);
            }
            return responseString;
        }

        protected async Task<T> Put<T, TY>(string path, TY obj, Dictionary<string, string> param = null)
        {
            string url = GetUrl(path, param);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("token", AccessToken);

            var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(obj)));
            string responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.ToString() + responseString);
            }
            var result = JsonConvert.DeserializeObject<T>(responseString);
            return result;

        }

        protected async Task<string> Put<T>(string path, T obj, Dictionary<string, string> param = null)
        {
            string url = GetUrl(path, param);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("token", AccessToken);

            var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(obj)));
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.ToString() + responseString);
            }
            return responseString;
        }

        protected async Task<string> Delete(string path, Dictionary<string, string> param = null)
        {
            string url = GetUrl(path, param);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("token", AccessToken);

            var response = await client.DeleteAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.ToString() + responseString);
            }
            return responseString;
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
    }
}
