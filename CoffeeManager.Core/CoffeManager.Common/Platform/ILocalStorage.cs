using System;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface ILocalStorage
    {
        UserInfo GetUserInfo();
        void SetUserInfo(UserInfo info);
        void ClearUserInfo();

    }
}
