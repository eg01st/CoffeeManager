using System.Windows.Input;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;
using System.Linq;
using CoffeManager.Common;
using System.Threading.Tasks;
using CoffeManager.Common.Managers;
using System;

namespace CoffeeManager.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserManager _userManager;
        private readonly IShiftManager _shiftManager;
        private User[] _users;

        public ICommand SelectUserCommand { get; }
        public ICommand RefreshCommand { get; }

        readonly IAccountManager accountManager;
        readonly ILocalStorage localStorage;

        public LoginViewModel(IShiftManager shiftManager, IUserManager userManager, IAccountManager accountManager, ILocalStorage localStorage)
        {
            this.localStorage = localStorage;
            this.accountManager = accountManager;
            _userManager = userManager;
            _shiftManager = shiftManager;
            SelectUserCommand = new MvxCommand<User>(DoSelectUser);
            RefreshCommand = new MvxAsyncCommand(DoRefresh);
        }

        private async Task DoRefresh()
        {
            await ExecuteSafe(async () =>
            {
                await Init();
            });
        }

        private async void DoSelectUser(User user)
        {
            var counter = await PromtAsync("Введите показание счетчика на кофемолке");
            if(!counter.HasValue)
            {
                return;
            }

            await ExecuteSafe(async () =>
            {
                int shiftId = await _shiftManager.StartUserShift(user.Id, counter.Value);
                ShowViewModel<MainViewModel>(new { userId = user.Id, shiftId = shiftId });
            });
        }

        public User[] Users
        {
            get { return _users; }
            set
            {
                _users = value;
                RaisePropertyChanged(nameof(Users));
            }
        }

        public async Task Init()
        {
            await ExecuteSafe(async () =>
            {
                var userInfo = localStorage.GetUserInfo();

                try
                {
                    if (userInfo == null)
                    {
                        await PromtLogin();
                    }
                    else
                    {
                        await accountManager.Authorize(userInfo.Login, userInfo.Password);
                    }
                }
                catch (System.UnauthorizedAccessException uex)
                {
                    Alert("Не правильный логин или пароль");
                    localStorage.ClearUserInfo();
                    return;
                }
                catch(Exception ex)
                {
                    Alert("Произошла ошибка сервера. Мы работаем над решением проблемы");
                    await EmailService?.SendErrorEmail(ex.ToDiagnosticString());
                    return;
                }

                Shift currentShift = await _shiftManager.GetCurrentShift();
                if (currentShift != null)
                {
                    ShowViewModel<MainViewModel>(new { userId = currentShift.UserId, shiftId = currentShift.Id });
                }
                var res = await _userManager.GetUsers();
                Users = res.Where(u => u.IsActive).ToArray();
            });
        }


        private async Task PromtLogin()
        {
            var email = await PromtStringAsync("Введите логин");
            if(string.IsNullOrEmpty(email))
            {
                return;
            }
            var password = await PromtStringAsync("Введите пароль");
            if (string.IsNullOrEmpty(password))
            {
                return;
            }
            await accountManager.Authorize(email, password);
            localStorage.SetUserInfo(new UserInfo() { Login = email, Password = password });
        }
    }
}
