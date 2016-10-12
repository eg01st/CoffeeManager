using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Platform;
using Newtonsoft.Json;

namespace CoffeeManager.Core.ServiceProviders
{
    public class BaseServiceProvider
    {
        protected IUserDialogs UserDialogs
        {
            get
            {
                return Mvx.Resolve<IUserDialogs>();
            }
        }

        protected readonly int CoffeeRoomNo = Config.CoffeeRoomNo;
        private readonly string _apiUrl = Config.ApiUrl;

        protected async Task PutInternal(string path, string obj)
        {
            RequestExecutor.Put(path, obj);
        }

        protected async Task PostInternal(string path, string obj)
        {
            RequestExecutor.Post(path, obj);
        }

        protected async Task<T> Get<T>(string path, Dictionary<string, string> param = null)
        {
            string url = string.Empty;
            try
            {
                var client = new HttpClient();

                url = $"{_apiUrl}{path}?coffeeroomno={CoffeeRoomNo}";
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
            catch (Exception ex)
            {
                RequestExecutor.LogError($"GET {path} {ex}");
                UserDialogs.Alert("Произошла ошибка запроса к серверу");
                throw;
            }
        }

        protected async Task<T> Post<T, TY>(string path, TY obj, Dictionary<string, string> param = null)
        {
            string url = string.Empty;
            try
            {
                var client = new HttpClient();
                url = $"{_apiUrl}{path}?coffeeroomno={CoffeeRoomNo}";
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
            catch (Exception ex)
            {
                RequestExecutor.LogError($"Post {path} {ex}");
                UserDialogs.Alert("Произошла ошибка запроса к серверу");
                throw;
            }
        }

        protected async Task<string> Post<T>(string path, T obj, Dictionary<string, string> param = null)
        {
            string url = string.Empty;
            try
            {
                var client = new HttpClient();
                url = $"{_apiUrl}{path}?coffeeroomno={CoffeeRoomNo}";
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
            catch (Exception ex)
            {
                RequestExecutor.LogError($"Post {path} {ex}");
                UserDialogs.Alert("Произошла ошибка запроса к серверу");
                throw;
            }
        }

        protected async Task<T> Put<T, TY>(string path, TY obj, Dictionary<string, string> param = null)
        {
            string url = string.Empty;
            try
            {
                var client = new HttpClient();
                url = $"{_apiUrl}{path}?coffeeroomno={CoffeeRoomNo}";
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
            catch (Exception ex)
            {
                RequestExecutor.LogError($"Put {path} {ex}");
                UserDialogs.Alert("Произошла ошибка запроса к серверу");
                throw;
            }
        }

        protected async Task<string> Put<T>(string path, T obj, Dictionary<string, string> param = null)
        {
            string url = string.Empty;
            try
            {
                var client = new HttpClient();
                url = $"{_apiUrl}{path}?coffeeroomno={CoffeeRoomNo}";
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
            catch (Exception ex)
            {
                RequestExecutor.LogError($"Put {path} {ex}");
                UserDialogs.Alert("Произошла ошибка запроса к серверу");
                throw;
            }
        }

        protected async Task<string> Delete(string path, Dictionary<string, string> param = null)
        {
            string url = string.Empty;
            try
            {
                var client = new HttpClient();
                url = $"{_apiUrl}{path}?coffeeroomno={CoffeeRoomNo}";
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
            catch (Exception ex)
            {
                RequestExecutor.LogError($"Delete {path} {ex}");
                UserDialogs.Alert("Произошла ошибка запроса к серверу");
                throw;
            }
        }
    }
}
