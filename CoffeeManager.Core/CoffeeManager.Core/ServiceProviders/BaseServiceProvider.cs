using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Platform;
using Newtonsoft.Json;
using MvvmCross.Plugins.Messenger;

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

        protected IMvxMessenger Messenger
        {
            get
            {
                return Mvx.Resolve<IMvxMessenger>();
            }
        }

        public static bool Ping()
        {
            try
            {
                var client = new HttpClient();
                var url = $"{_apiUrl}users?coffeeroomno={CoffeeRoomNo}";
                var response = client.GetStringAsync(url);

                string responseString = response.Result;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        protected static readonly int CoffeeRoomNo = Config.CoffeeRoomNo;
        private static readonly string _apiUrl = Config.ApiUrl;

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
            UserDialogs.ShowLoading("Loading", Acr.UserDialogs.MaskType.Black);
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
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(responseString);
                }
                var result = JsonConvert.DeserializeObject<T>(responseString);
                return result;

            }
            catch (Exception ex)
            {
                RequestExecutor.LogError($"{DateTime.Now}: GET {path} Error: {ex}");
                UserDialogs.Alert("Произошла ошибка запроса к серверу");
                Messenger.Publish(new LostConnectionMessage(this));
                return default(T);
                //throw;
            }
            finally
            {
                UserDialogs.HideLoading();
            }
        }

        protected async Task<T> Post<T, TY>(string path, TY obj, Dictionary<string, string> param = null)
        {
            UserDialogs.ShowLoading("Loading", Acr.UserDialogs.MaskType.Black);
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
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(responseString);
                }
                var result = JsonConvert.DeserializeObject<T>(responseString);
                return result;
            }
            catch (Exception ex)
            {
                RequestExecutor.LogError($"{DateTime.Now}: Post {path} Error: {ex}");
                UserDialogs.Alert("Произошла ошибка запроса к серверу");
                Messenger.Publish(new LostConnectionMessage(this));
                return default(T);
                //throw;
            }
            finally
            {
                UserDialogs.HideLoading();
            }
        }

        protected async Task<string> Post<T>(string path, T obj, Dictionary<string, string> param = null)
        {
            UserDialogs.ShowLoading("Loading", Acr.UserDialogs.MaskType.Black);
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
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(responseString);
                }
                return responseString;
            }
            catch (Exception ex)
            {
                RequestExecutor.LogError($"{DateTime.Now}: Post {path} Error: {ex}");
                UserDialogs.Alert("Произошла ошибка запроса к серверу");
                Messenger.Publish(new LostConnectionMessage(this));
                return null;
                //throw;
            }
            finally
            {
                UserDialogs.HideLoading();
            }
        }

        protected async Task<T> Put<T, TY>(string path, TY obj, Dictionary<string, string> param = null)
        {
            UserDialogs.ShowLoading("Loading", Acr.UserDialogs.MaskType.Black);
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
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(responseString);
                }
                var result = JsonConvert.DeserializeObject<T>(responseString);
                return result;
            }
            catch (Exception ex)
            {
                RequestExecutor.LogError($"{DateTime.Now}: Put {path} Error: {ex}");
                UserDialogs.Alert("Произошла ошибка запроса к серверу");
                Messenger.Publish(new LostConnectionMessage(this));
                return default(T);
                //throw;
            }
            finally
            {
                UserDialogs.HideLoading();
            }
        }

        protected async Task<string> Put<T>(string path, T obj, Dictionary<string, string> param = null)
        {
            UserDialogs.ShowLoading("Loading", Acr.UserDialogs.MaskType.Black);
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
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(responseString);
                }
                return responseString;
            }
            catch (Exception ex)
            {
                RequestExecutor.LogError($"{DateTime.Now}: Put {path} Error: {ex}");
                UserDialogs.Alert("Произошла ошибка запроса к серверу");
                Messenger.Publish(new LostConnectionMessage(this));
                return null;
                // throw;
            }
            finally
            {
                UserDialogs.HideLoading();
            }
        }

        protected async Task<string> Delete(string path, Dictionary<string, string> param = null)
        {
            UserDialogs.ShowLoading("Loading", Acr.UserDialogs.MaskType.Black);
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
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(responseString);
                }
                return responseString;
            }
            catch (Exception ex)
            {
                RequestExecutor.LogError($"{DateTime.Now}: Delete {path} Error: {ex}");
                UserDialogs.Alert("Произошла ошибка запроса к серверу");
                Messenger.Publish(new LostConnectionMessage(this));
                return null;
                //throw;
            }
            finally
            {
                UserDialogs.HideLoading();
            }
        }
    }
}
