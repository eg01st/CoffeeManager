using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;
using MvvmCross.Platform;
using MvvmCross.Plugins.File;
using Newtonsoft.Json;

namespace CoffeeManager.Core
{
    public class RequestExecutor
    {
        protected static readonly int CoffeeRoomNo = 1; //Take from config later
        private static readonly string _apiUrl = "http://coffeeroom.ddns.net:8082/api/";  //"http://169.254.80.80:8080/api/"; //Todo: init from configfile


        private static IMvxFileStore storage = Mvx.Resolve<IMvxFileStore>();
        private const string FileName = "RequestsQueue";
        private static bool _timerRunning = true;

        public static void Run()
        {
                var requestStorage = GetStorage();
                if (requestStorage.Requests.Any())
                {
                    foreach (var request in requestStorage.Requests)
                    {
                        try
                        {
                            RunInternal(request);
                            requestStorage.Requests.Remove(request);
                            SaveStorage(requestStorage);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }

                    }
                }
        }
        private static RequestStorage GetStorage()
        {
            string storageJson;
            if(storage.TryReadTextFile(FileName, out storageJson))
            {
                return JsonConvert.DeserializeObject<RequestStorage>(storageJson);
            }
            else
            {
                return new RequestStorage() {Requests = new List<Request>()};
            }
        }

        private static void SaveStorage(RequestStorage requestStorage)
        {
            storage.WriteFile(FileName, JsonConvert.SerializeObject(requestStorage));
        }

        public static Task Post(string path, string obj)
        {
            var requestStorage = GetStorage();
            requestStorage.Requests.Add(new Request() {Method = "POST", Path = path, ObjectJson = obj});
            storage.WriteFile(FileName, JsonConvert.SerializeObject(requestStorage));
            return null;
        }

        public static void Put(string path, string obj)
        {
            var requestStorage = GetStorage();
            requestStorage.Requests.Add(new Request() { Method = "PUT", Path = path, ObjectJson = obj });
            storage.WriteFile(FileName, JsonConvert.SerializeObject(requestStorage));
        }

        private static void RunInternal(Request request)
        {
            var client = new HttpClient();
            string url = $"{_apiUrl}{request.Path}?coffeeroomno={CoffeeRoomNo}";
            if (request.Method == "PUT")
            {
                var response = client.PutAsync(url, new StringContent(request.ObjectJson)).Result;
                response.Content.ReadAsStringAsync();
            }
            else
            {
                var response = client.PostAsync(url, new StringContent(request.ObjectJson)).Result;
                response.Content.ReadAsStringAsync();
            }
        }
    }
}
