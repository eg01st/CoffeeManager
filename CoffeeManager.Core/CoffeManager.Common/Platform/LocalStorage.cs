using System;
using CoffeeManager.Models;
using MvvmCross.Plugins.File;
using Newtonsoft.Json;

namespace CoffeManager.Common
{
    public class LocalStorage : ILocalStorage
    {
        private const string UserInfoStorage = "UserInfo";
        readonly IMvxFileStore storage;

        public LocalStorage(IMvxFileStore storage)
        {
            this.storage = storage;
        }

        public UserInfo GetUserInfo()
        {
            var info = GetStorage<UserInfo>(UserInfoStorage);
            return info;
        }

        public void ClearUserInfo()
        {
            storage.WriteFile(UserInfoStorage, string.Empty);
        }

        public void SetUserInfo(UserInfo info)
        {
            storage.WriteFile(UserInfoStorage, JsonConvert.SerializeObject(info));
        }

        private T GetStorage<T>(string fileName) where T : new()
        {
            string storageJson;
            if (storage.TryReadTextFile(fileName, out storageJson))
            {
                return JsonConvert.DeserializeObject<T>(storageJson);
            }
            else
            {
                return default(T);
            }
        }
    }
}
