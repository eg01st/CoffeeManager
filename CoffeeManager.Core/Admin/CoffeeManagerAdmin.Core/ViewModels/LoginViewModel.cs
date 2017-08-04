﻿using System;
using System.Windows.Input;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserManager manager;

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


        public LoginViewModel(IUserManager manager)
        {
            this.manager = manager;
            _loginCommand = new MvxCommand(DoLogin);

            var userinfo = LocalStorage.GetUserInfo();
            Name = userinfo.Login;
            Password = userinfo.Password;
        }

        public ICommand LoginCommand => _loginCommand;

        private async void DoLogin()
        {
            await ExecuteSafe(async () => 
            {           
                string accessToken = await manager.Login(Name, Password);
                accessToken = accessToken.Substring(1);
                accessToken = accessToken.Substring(0, accessToken.Length - 1);
                LocalStorage.SetUserInfo(new UserInfo() { Login = Name, Password = Password });
                BaseServiceProvider.AccessToken = accessToken;
                ShowViewModel<MainViewModel>();
            });

        }
    }
}
