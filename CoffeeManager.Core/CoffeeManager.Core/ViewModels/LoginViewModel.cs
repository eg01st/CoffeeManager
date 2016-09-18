using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private UserManager manager;

        public LoginViewModel()
        {
            manager = new UserManager();
        }

        public User[] Users;

        public void Init()
        {
            Users = manager.GetUsers();
        }

        public string Prope => "Test Filed";
    }
}
