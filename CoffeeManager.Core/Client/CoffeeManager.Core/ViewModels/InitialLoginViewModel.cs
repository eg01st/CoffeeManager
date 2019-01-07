using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Common;
using CoffeeManager.Core.ViewModels.Settings;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MobileCore;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class InitialLoginViewModel : ViewModelBase, IMvxViewModelResult<bool>
    {
        private string login;
        private string password;

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                RaisePropertyChanged(nameof(Login));
                RaisePropertyChanged(nameof(LoginCommand));
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged(nameof(Password));
                RaisePropertyChanged(nameof(LoginCommand));
            }
        }

        public ICommand LoginCommand { get; }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        readonly ILocalStorage localStorage;
        readonly IAccountManager accountManager;

        public InitialLoginViewModel(ILocalStorage localStorage, IAccountManager accountManager)
        {
            this.accountManager = accountManager;
            this.localStorage = localStorage;
            LoginCommand = new MvxAsyncCommand(DoLogin, CanLogin);
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
        }

        public override Task Initialize()
        {
            var userInfo = localStorage.GetUserInfo();
            Login = userInfo?.Login;
            
            return Task.FromResult(true);
        }

        private async Task DoLogin()
        {
            try
            {
                var token = await accountManager.Authorize(Login, Password);
                localStorage.SetUserInfo(new Models.UserInfo(){Login = Login, Password = Password, AccessToken = token});
                var coffeeRoomNo = localStorage.GetCoffeeRoomId();
                if (coffeeRoomNo == -1)
                {
                    await NavigationService.Navigate<SettingsViewModel>();
                }
                else
                {
                    Config.CoffeeRoomNo = coffeeRoomNo;
                    await NavigationService.Close(this, true);
                }
            }
            catch (System.UnauthorizedAccessException uex)
            {
                Alert("Не правильный логин или пароль");
                localStorage.ClearUserInfo();
            }
            catch (Exception ex)
            {
                Alert("Произошла ошибка сервера. Мы работаем над решением проблемы");
                await EmailService?.SendErrorEmail($"CoffeeRoomId: {Config.CoffeeRoomNo}", ex.ToDiagnosticString());
            }
        }

        protected override async Task DoClose()
        {
            await NavigationService.Close(this, false);
        }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (CloseCompletionSource != null && !CloseCompletionSource.Task.IsCompleted && !CloseCompletionSource.Task.IsFaulted)
                CloseCompletionSource?.TrySetCanceled();
            base.ViewDestroy(viewFinishing);
        }
    }
}
