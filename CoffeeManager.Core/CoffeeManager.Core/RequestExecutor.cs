﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
        protected static readonly int CoffeeRoomNo = Config.CoffeeRoomNo;
        private static readonly string _apiUrl = Config.ApiUrl;
        private static readonly object sync = new object();

        private static IMvxFileStore storage = Mvx.Resolve<IMvxFileStore>();
        private const string RequestQueue = "RequestsQueue";
        private const string ErrorsLog = "ErrorsLog";

        public static void Run()
        {
            lock (sync)
            {
                var requestStorage = GetStorage();

                while (requestStorage.Requests.Any(r => string.IsNullOrEmpty(r.ErrorMessage)))
                {
                    var request = requestStorage.Requests.First(r => string.IsNullOrEmpty(r.ErrorMessage));
                    try
                    {
                        var ex = RunInternal(request).Result;
                        Debug.WriteLine($"REQUESTEXECUTOR: {request.Method} {request.Path} {request.ObjectJson}");
                        if (ex != null)
                        {
                            throw ex;
                        }
                        requestStorage.Requests.Remove(request);
                    }
                    catch (Exception ex)
                    {
                        request.ErrorMessage = $"{request.Method} {request.Path} {request.ObjectJson} {ex}";
                        continue;
                    }
                    finally
                    {
                        SaveStorage(requestStorage);
                    }
                }
                requestStorage = GetStorage();
                foreach (var request in requestStorage.Requests)
                {
                    try
                    {
                        var ex = RunInternal(request).Result;
                        Debug.WriteLine($"REQUESTEXECUTOR: {request.Method} {request.Path} {request.ObjectJson}");
                        if (ex != null)
                        {
                            throw ex;
                        }
                        requestStorage.Requests.Remove(request);
                    }
                    catch (Exception ex)
                    {
                        request.ErrorMessage = $"{request.Method} {request.Path} {request.ObjectJson} {ex}";
                        continue;
                    }
                    finally
                    {
                        SaveStorage(requestStorage);
                    }
                }
            }
        }
        private static RequestStorage GetStorage()
        {
            string storageJson;
            if(storage.TryReadTextFile(RequestQueue, out storageJson) && !string.IsNullOrWhiteSpace(storageJson))
            {
                return JsonConvert.DeserializeObject<RequestStorage>(storageJson);
            }
            else
            {
                return new RequestStorage() {Requests = new List<Request>()};
            }
        }

        private static RequestStorage GetErrorStorage()
        {
            string storageJson;
            if (storage.TryReadTextFile(ErrorsLog, out storageJson))
            {
                return JsonConvert.DeserializeObject<RequestStorage>(storageJson);
            }
            else
            {
                return new RequestStorage() {Requests = new List<Request>(), Errors = new List<string>()};
            }
        }

        private static void SaveStorage(RequestStorage requestStorage)
        {
            storage.WriteFile(RequestQueue, JsonConvert.SerializeObject(requestStorage));
        }

        private static void SaveErrorStorage(RequestStorage requestStorage)
        {
            storage.WriteFile(ErrorsLog, JsonConvert.SerializeObject(requestStorage));
        }

        public static void Post(string path, string obj)
        {
            var requestStorage = GetStorage();
            requestStorage.Requests.Add(new Request() {Method = "POST", Path = path, ObjectJson = obj});
            storage.WriteFile(RequestQueue, JsonConvert.SerializeObject(requestStorage));
        }

        public static void Put(string path, string obj)
        {
            var requestStorage = GetStorage();
            requestStorage.Requests.Add(new Request() { Method = "PUT", Path = path, ObjectJson = obj });
            storage.WriteFile(RequestQueue, JsonConvert.SerializeObject(requestStorage));
        }

        private static async Task<Exception> RunInternal(Request request)
        {
            var client = new HttpClient();
            string url = $"{_apiUrl}{request.Path}?coffeeroomno={CoffeeRoomNo}";
            try
            {
                if (request.Method == "PUT")
                {
                    var response = await client.PutAsync(url, new StringContent(request.ObjectJson));
                    var res = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(res);
                    }
                }
                else
                {
                    var response = await client.PostAsync(url, new StringContent(request.ObjectJson));
                    var res = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(res);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static void LogError(string message)
        {
            var st = GetErrorStorage();
            st.Errors.Add(message);
            SaveErrorStorage(st);
        }

        public static void ClearErrors()
        {
            var st = GetErrorStorage();
            st.Errors.Clear();
            SaveErrorStorage(st);
        }

        public static List<Request> GetRequests()
        {
            return GetStorage().Requests;
        }

        public static List<string> GetErrors()
        {
            return GetErrorStorage().Errors;
        }
    }
}
