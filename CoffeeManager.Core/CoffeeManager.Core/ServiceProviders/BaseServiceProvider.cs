using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CoffeeManager.Core.ServiceProviders
{
    public class BaseServiceProvider
    {
        protected readonly int CoffeeRoomNo = 1; //Take from config later
        private readonly string _apiUrl = "http://192.168.8.50:8080/api/";  //"http://169.254.80.80:8080/api/"; //Todo: init from configfile

        protected async Task<T> Get<T>(string path, Dictionary<string, string> param = null)
        {
            var client = new HttpClient();

            string url = $"{_apiUrl}{path}?coffeeroomno={CoffeeRoomNo}";
            if (param != null && param.Count > 0)
            {
                foreach (var parameter in param)
                {
                    url += $"&{parameter.Key}={parameter.Value}";
                }
            }
            var response = await client.GetAsync(url);
            string responseString = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(responseString);
            var result = JsonConvert.DeserializeObject<T>(responseString);
            return result;
        }

        protected async Task<T> Post<T, TY>(string path, TY obj, Dictionary<string, string> param = null)
        {
            var client = new HttpClient();
            string url = $"{_apiUrl}{path}?coffeeroomno={CoffeeRoomNo}";
            if (param != null && param.Count > 0)
            {
                foreach (var parameter in param)
                {
                    url += $"&{parameter.Key}={parameter.Value}";
                }
            }
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(obj)));
            string responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(responseString);
            return result;
        }

        protected async Task<string> Post<T>(string path, T obj, Dictionary<string, string> param = null)
        {
            var client = new HttpClient();
            string url = $"{_apiUrl}{path}?coffeeroomno={CoffeeRoomNo}";
            if (param != null && param.Count > 0)
            {
                foreach (var parameter in param)
                {
                    url += $"&{parameter.Key}={parameter.Value}";
                }
            }
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(obj)));
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<T> Put<T, TY>(string path, TY obj, Dictionary<string, string> param = null)
        {
            var client = new HttpClient();
            string url = $"{_apiUrl}{path}?coffeeroomno={CoffeeRoomNo}";
            if (param != null && param.Count > 0)
            {
                foreach (var parameter in param)
                {
                    url += $"&{parameter.Key}={parameter.Value}";
                }
            }
            var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(obj)));
            string responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(responseString);
            return result;
        }

        protected async Task<string> Put<T>(string path, T obj, Dictionary<string, string> param = null)
        {
            var client = new HttpClient();
            string url = $"{_apiUrl}{path}?coffeeroomno={CoffeeRoomNo}";
            if (param != null && param.Count > 0)
            {
                foreach (var parameter in param)
                {
                    url += $"&{parameter.Key}={parameter.Value}";
                }
            }
            var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(obj)));
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<string> Delete(string path, Dictionary<string, string> param = null)
        {
            var client = new HttpClient();
            string url = $"{_apiUrl}{path}?coffeeroomno={CoffeeRoomNo}";
            if (param != null && param.Count > 0)
            {
                foreach (var parameter in param)
                {
                    url += $"&{parameter.Key}={parameter.Value}";
                }
            }
            var response = await client.DeleteAsync(url);
            return await response.Content.ReadAsStringAsync();

        }
    }
}
