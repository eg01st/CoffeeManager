using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common.Managers
{
    public interface IAccountManager
    {
        Task<string> AuthorizeInitial(string login, string password);

        Task<string> Authorize(string login, string password);

        Task<UserAcount> GetUserInfo();

        Task Register(string email, string password);

        Task Logout();

        Task ChangePassword(string oldPassword, string newPassword);

        Task SetPassword(string newPassword, string confirmPassword);
    }
}
