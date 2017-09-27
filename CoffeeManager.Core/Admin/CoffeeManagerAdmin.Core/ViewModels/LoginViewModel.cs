using System;
using System.Windows.Input;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeManager.Common.Managers;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserManager manager;
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


        public LoginViewModel(IUserManager manager, IAccountManager accountManager)
        {
            this.manager = manager;
            _accountManager = accountManager;
            _loginCommand = new MvxAsyncCommand(DoLogin);
        }

        public async Task Init()
        {
            var userinfo = LocalStorage.GetUserInfo();
            Name = userinfo.Login;
            Password = userinfo.Password;

            await DoLogin();
        }

        public ICommand LoginCommand => _loginCommand;

        private async Task DoLogin()
        {
            await ExecuteSafe(async () => 
            {           
                string accessToken = await _accountManager.AuthorizeInitial(Name, Password);
                BaseServiceProvider.SetAccessToken(accessToken);
                var user = await _accountManager.GetUserInfo();
                Config.ApiUrl = user.ApiUrl;

                accessToken = await _accountManager.Authorize(Name, Password);
                BaseServiceProvider.SetAccessToken(accessToken);

                //accessToken = accessToken.Substring(1);
                //accessToken = accessToken.Substring(0, accessToken.Length - 1);
                LocalStorage.SetUserInfo(new UserInfo() { Login = Name, Password = Password });
                BaseServiceProvider.SetAccessToken(accessToken);
                ShowViewModelAsRoot<MainViewModel>();
            });

        }
    }
}
