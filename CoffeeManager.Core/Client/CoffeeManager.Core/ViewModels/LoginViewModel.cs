using System.Windows.Input;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;
using System.Linq;
using CoffeManager.Common;
using System.Threading.Tasks;
using CoffeManager.Common.Managers;
using System;
using CoffeeManager.Common;
using CoffeeManager.Models.Data.DTO.User;
using CoffeManager.Common.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserManager _userManager;
        private readonly IShiftManager _shiftManager;
        private UserDTO[] _users;

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
            SelectUserCommand = new MvxCommand<UserDTO>(DoSelectUser);
            RefreshCommand = new MvxAsyncCommand(DoRefresh);
        }

        private async Task DoRefresh()
        {
            await ExecuteSafe(async () =>
            {
                await Initialize();
            });
        }

        private async void DoSelectUser(UserDTO user)
        {
            var counter = await PromtAsync("Введите показание счетчика на кофемолке");
            if(!counter.HasValue)
            {
                return;
            }

            var confirm = await PromtAsync("Повторите показание счетчика на кофемолке");
            if(!confirm.HasValue)
            {
                return;
            }

            if (!string.Equals(counter, confirm))
            {
                Alert("Показания введены неверно, введите правильные показания счетчика");
                return;
            }

            await ExecuteSafe(async () =>
            {
                int shiftId = await _shiftManager.StartUserShift(user.Id, counter.Value);
                await NavigationService.Navigate<MainViewModel, Shift>(new Shift {  UserId = user.Id, Id = shiftId });
            });
        }

        public UserDTO[] Users
        {
            get { return _users; }
            set
            {
                _users = value;
                RaisePropertyChanged(nameof(Users));
            }
        }

        public async override Task Initialize()
        {
            var res = await ExecuteSafe(_userManager.GetUsers);
            Users = res?.Where(u => u.IsActive).ToArray();
        }

       

    }
}
