using System.Windows.Input;
using CoffeeManager.Core.Managers;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;
        private readonly ShiftManager _shiftManager; 
        private readonly ICommand _selectUserCommand;
        private User[] _users;

        public ICommand SelectUserCommand => _selectUserCommand;

        public LoginViewModel()
        {
            _userManager = new UserManager();
            _shiftManager = new ShiftManager();
            _selectUserCommand = new MvxCommand<User>(DoSelectUser);
        }

        private void DoSelectUser(User user)
        {
            _shiftManager.StartUserShift(user.Id);
            ShowViewModel<MainViewModel>();
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

        public void Init()
        {
            Users = _userManager.GetUsers();
        }
    }
}
