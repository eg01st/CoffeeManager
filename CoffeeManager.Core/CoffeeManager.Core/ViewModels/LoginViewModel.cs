using System.Windows.Input;
using CoffeeManager.Core.Managers;
using CoffeeManager.Models;
using CoffeeManager.Models.Interfaces;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;
        private readonly ShiftManager _shiftManager; 
        private readonly ICommand _selectUserCommand;
        private IUser[] _users;

        public ICommand SelectUserCommand => _selectUserCommand;

        public LoginViewModel()
        {
            _userManager = new UserManager();
            _shiftManager = new ShiftManager();
            _selectUserCommand = new MvxCommand<User>(DoSelectUser);
        }

        private async void DoSelectUser(User user)
        {
            int shiftId = await _shiftManager.StartUserShift(user.Id);
            ShowViewModel<MainViewModel>( new {userId = user.Id, shiftId = shiftId });
        }

        public IUser[] Users
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
            Shift currentShift = await _shiftManager.GetCurrentShift();
            if (currentShift != null)
            {
                ShowViewModel<MainViewModel>(new { userId = currentShift.UserId, shiftId = currentShift.Id });
            }

            Users = await _userManager.GetUsers();
        }
    }
}
