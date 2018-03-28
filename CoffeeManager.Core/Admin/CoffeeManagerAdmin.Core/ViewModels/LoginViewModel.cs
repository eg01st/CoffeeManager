﻿using System;
using System.Windows.Input;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAccountManager _accountManager;

        private string _name;
        private string _password;
        private ICommand _loginCommand;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        readonly ILocalStorage localStorage;

        public LoginViewModel(IAccountManager accountManager, ILocalStorage localStorage)
        {
            this.localStorage = localStorage;
            _accountManager = accountManager;
            _loginCommand = new MvxAsyncCommand(DoLogin);
        }

        public override async Task Initialize()
        {
            var userinfo = localStorage.GetUserInfo();
            Name = userinfo.Login;
            Password = userinfo.Password;

            await DoLogin();
        }

        public ICommand LoginCommand => _loginCommand;

        private async Task DoLogin()
        {
            await ExecuteSafe(async () => 
            {           
                var token = await _accountManager.Authorize(Name, Password);

                localStorage.SetUserInfo(new UserInfo() { Login = Name, Password = Password, AccessToken = token });
                await NavigationService.Navigate<MainViewModel>();
                //ShowViewModelAsRoot<MainViewModel>();
            });

        }
    }
}
