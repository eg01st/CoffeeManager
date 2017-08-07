using System.Windows.Input;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;
using System.Linq;
using CoffeManager.Common;
using System.Threading.Tasks;

namespace CoffeeManager.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserManager _userManager;
        private readonly IShiftManager _shiftManager;
        private User[] _users;

        public ICommand SelectUserCommand { get; }
        public ICommand RefreshCommand { get; }

        public LoginViewModel(IShiftManager shiftManager, IUserManager userManager)
        {
            _userManager = userManager;
            _shiftManager = shiftManager;
            SelectUserCommand = new MvxCommand<User>(DoSelectUser);
            RefreshCommand = new MvxAsyncCommand(DoRefresh);
        }

        private async Task DoRefresh()
        {
            await ExecuteSafe(async () =>
            {
                var res = await _userManager.GetUsers();
                Users = res.Where(u => u.IsActive).ToArray();
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

        public async void Init()
        {
            await ExecuteSafe(async () =>
            {
                Shift currentShift = await _shiftManager.GetCurrentShift();
                if (currentShift != null)
                {
                    ShowViewModel<MainViewModel>(new { userId = currentShift.UserId, shiftId = currentShift.Id });
                }
                var res = await _userManager.GetUsers();
                Users = res.Where(u => u.IsActive).ToArray();
            });
        }
    }
}
